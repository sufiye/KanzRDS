import Navbar from "../components/Navbar"
import Footer from "../components/Footer"
import { useState, useEffect } from "react"
import api from "../utils/axios"
import { useDarkmode } from "../stores/store"
import { useNavigate } from "react-router-dom"
import { useTokens } from "../stores/tokenStore"

const Order = () => {
  const { isDarkmodeEnabled } = useDarkmode()
  const { roles } = useTokens()
  const isAdmin = roles?.includes("Admin")

  const [orders, setOrders] = useState([])
  const [products, setProducts] = useState({})
  const [users, setUsers] = useState({})

  const navigate = useNavigate()

  const normalizeArray = (data) => {
    if (Array.isArray(data)) return data
    if (Array.isArray(data?.items)) return data.items
    return []
  }

  // 🔥 PRODUCT SAFE
  const getProductById = async (id) => {
    try {
      if (!id || products[id]) return

      const res = await api.get(`Product/${id}`)
      const data = res?.data

      setProducts(prev => ({
        ...prev,
        [id]: data || {}
      }))
    } catch (error) {
      console.error("PRODUCT ERROR:", error)
    }
  }

  // 🔥 USER SAFE
  const getUserById = async (userId) => {
    try {
      if (!userId || users[userId]) return

      const res = await api.post(`/Auth/${userId}/user`)

      setUsers(prev => ({
        ...prev,
        [userId]: res?.data || {}
      }))

    } catch (err) {
      console.error("USER ERROR:", err)
    }
  }

  // 🔥 ORDERS SAFE
  const getOrders = async () => {
    try {
      const url = isAdmin ? "/Order/All" : "/Order"

      const res = await api.get(url)
      const data = normalizeArray(res?.data)

      setOrders(data)

      data.forEach(order => {
        order?.items?.forEach(item => {
          getProductById(item?.productId)
        })

        if (order?.userId) {
          getUserById(order.userId)
        }
      })

    } catch (error) {
      console.error("ORDER ERROR:", error)
    }
  }

  // 🔥 STATUS UPDATE FIX (SƏNİ SINDIRAN BU İDİ)
  const updateStatus = async (id, status) => {
    try {
      await api.put(`/Order/${id}`, { status })

      setOrders(prev =>
        prev.map(o =>
          o.id === id ? { ...o, status } : o
        )
      )
    } catch (error) {
      console.error("STATUS ERROR:", error)
    }
  }

  useEffect(() => {
    const token = localStorage.getItem("tokens")

    if (!token) {
      navigate("/login")
      return
    }

    getOrders()
  }, [isAdmin])

  return (
    <div className={`min-h-screen w-full ${
      isDarkmodeEnabled
        ? "bg-[#1c1814] text-[#e6dccf]"
        : "bg-[#f4efe7] text-[#3a3835]"
    }`}>

      <Navbar />

      <div className="max-w-5xl mx-auto py-10 px-5">

        <h1 className="text-2xl font-semibold mb-6 tracking-wide">
          {isAdmin ? "All Orders" : "My Orders"} ({orders.length})
        </h1>

        {orders.length === 0 ? (
          <p>No orders found</p>
        ) : (
          <div className="space-y-6">

            {orders.map((order) => {

              const user = users[order?.userId]

              return (
                <div key={order?.id}
                  className={`p-5 rounded-xl border shadow-sm
                  ${isDarkmodeEnabled
                    ? "bg-[#26221d] border-[#3a342c]"
                    : "bg-white border-[#e5e0d8]"}`}>

                  {isAdmin && (
                    <div className="mb-3 text-sm space-y-1">
                      <p>
                        <b>User:</b>{" "}
                        {user?.firstName
                          ? user.firstName + " " + user.lastName
                          : "Loading..."}
                      </p>
                      <p><b>Phone:</b> {user?.phoneNumber || "-"}</p>
                      <p><b>Address:</b> {user?.address || "-"}</p>
                    </div>
                  )}

                  <div className="flex justify-between mb-2">
                    <span className="text-xs opacity-70">
                      {order?.id}
                    </span>

                    <span className={`text-xs px-3 py-1 rounded-full
                      ${
                        order?.status === "Pending"
                          ? "bg-yellow-500 text-white"
                          : order?.status === "Shipped"
                          ? "bg-blue-500 text-white"
                          : "bg-green-500 text-white"
                      }`}>
                      {order?.status}
                    </span>
                  </div>

                  <p className="text-sm opacity-70">
                    {order?.orderDate
                      ? new Date(order.orderDate).toLocaleString()
                      : ""}
                  </p>

                  <p className="font-semibold mt-1">
                    Total: {order?.totalPrice} AZN
                  </p>

                  <div className="mt-4 space-y-3">
                    {order?.items?.map((item, i) => {
                      const product = products[item?.productId]

                      return (
                        <div key={i}
                          className="flex justify-between items-center border-t pt-3 text-sm">

                          <div className="flex items-center gap-3">
                            <img
                              src={
                                product?.attachments?.length > 0
                                  ? `/Attachment/${product?.id}`
                                  : "/no-image.png"
                              }
                              className="w-12 h-12 object-cover rounded-lg"
                            />

                            <div>
                              <p className="font-medium">
                                {product?.name || "Loading..."}
                              </p>
                              <p className="text-xs opacity-60">
                                {item?.price} AZN
                              </p>
                            </div>
                          </div>

                          <span>x{item?.quantity}</span>
                        </div>
                      )
                    })}
                  </div>

                  {isAdmin && (
                    <div className="flex gap-3 mt-4 items-center">

                      <select
                        value={order?.status}
                        onChange={(e) =>
                          updateStatus(order.id, e.target.value)
                        }
                        className="border px-3 py-1 text-sm rounded"
                      >
                        <option value="Pending">Pending</option>
                        <option value="Shipped">Shipped</option>
                        <option value="Delivered">Delivered</option>
                      </select>

                    </div>
                  )}

                </div>
              )
            })}

          </div>
        )}

      </div>

      <Footer />
    </div>
  )
}

export default Order