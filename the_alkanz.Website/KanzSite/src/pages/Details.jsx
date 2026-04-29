import { useParams, useNavigate } from "react-router-dom"
import { useState, useEffect } from "react"
import Navbar from "../components/Navbar"
import Footer from "../components/Footer"
import { useDarkmode } from "../stores/store"
import { useTokens } from "../stores/tokenStore"
import api from "../utils/axios"

const Details = () => {
  const { isDarkmodeEnabled } = useDarkmode()
  const { accessToken, roles } = useTokens()
  const navigate = useNavigate()

  const isAdmin = roles?.includes("Admin")

  const [product, setProduct] = useState({})
  const [category, setCategory] = useState("")
  const [categories, setCategories] = useState([])

  const [images, setImages] = useState([])
  const [currentIndex, setCurrentIndex] = useState(0)

  const [quantity, setQuantity] = useState(1)
  const [added, setAdded] = useState(false)

  const [showEdit, setShowEdit] = useState(false)
  const [showImageModal, setShowImageModal] = useState(false)
  const [editImage, setEditImage] = useState(null)

  const [form, setForm] = useState({
    name: "",
    title: "",
    description: "",
    categoryId: "",
    price: 0,
    stockCount: 0,
  })

  const { id } = useParams()

  const isOutOfStock = product?.stockCount === 0

  const normalizeArray = (data) => {
    if (Array.isArray(data)) return data
    if (Array.isArray(data?.items)) return data.items
    if (Array.isArray(data?.data)) return data.data
    return []
  }

  const getProduct = async () => {
    try {
      const res = await api.get(`Product/${id}`)
      const data = res.data

      setProduct(data)

      setForm({
        name: data?.name || "",
        title: data?.title || "",
        description: data?.description || "",
        categoryId: data?.categoryId || "",
        price: data?.price || 0,
        stockCount: data?.stockCount || 0,
      })

      if (data?.categoryId) getCategory(data.categoryId)

      const imgs = normalizeArray(data?.attachments).map((att) => ({
        url: att?.imgUrl || "",
        id: att?.id,
      }))

      setImages(imgs)
      setCurrentIndex(0)

    } catch (error) {
      console.error("PRODUCT ERROR:", error)
    }
  }

  const getCategory = async (id) => {
    try {
      if (!id) return
      const res = await api.get(`Category/${id}`)
      setCategory(res.data?.name || "")
    } catch (error) {
      console.error(error)
    }
  }

  const getCategories = async () => {
    try {
      const res = await api.get("/Category")
      setCategories(normalizeArray(res.data))
    } catch (error) {
      console.error(error)
    }
  }

  const uploadImage = async () => {
    try {
      if (!editImage) return alert("Select image!")

      const formData = new FormData()
      formData.append("File", editImage)

      await api.post(`/Attachment/${id}`, formData, {
        headers: { "Content-Type": "multipart/form-data" },
      })

      setEditImage(null)
      await getProduct()

    } catch (err) {
      console.error("UPLOAD ERROR:", err?.response?.data || err)
    }
  }

  const deleteImage = async (imageId) => {
    try {
      await api.delete(`/Attachment/${imageId}`)
      setImages((prev) => prev.filter((img) => img.id !== imageId))
      setCurrentIndex(0)
    } catch (err) {
      console.error(err)
    }
  }

  const addToBasket = async () => {
    if (!accessToken) return navigate("/login")
    if (isOutOfStock) return

    if (quantity > (product?.stockCount || 0)) {
      alert("Stock limit exceeded!")
      return
    }

    await api.post("/BasketItem", {
      productId: product.id,
      quantity,
    })

    setAdded(true)
    setTimeout(() => setAdded(false), 1500)
  }

  const updateProduct = async () => {
    await api.put(`/Product/${id}`, {
      ...form,
      price: Number(form.price),
      stockCount: Number(form.stockCount),
    })

    setShowEdit(false)
    getProduct()
  }

  useEffect(() => {
    getProduct()
    getCategories()
  }, [id])

  return (
    <div className={`${isDarkmodeEnabled ? "bg-[#1c1814] text-white" : "bg-[#f4efe7] text-black"} min-h-screen`}>
      <Navbar />

      <div className="max-w-6xl mx-auto grid md:grid-cols-2 gap-10 p-6">

        <div className="relative">
          <img
            src={images[currentIndex]?.url || "/no-image.png"}
            className="h-[400px] w-full object-cover rounded-2xl"
          />

          {images.length > 1 && (
            <>
              <button onClick={() => setCurrentIndex((p) => (p - 1 + images.length) % images.length)}
                className="absolute left-2 top-1/2 bg-black/50 text-white px-2">‹</button>

              <button onClick={() => setCurrentIndex((p) => (p + 1) % images.length)}
                className="absolute right-2 top-1/2 bg-black/50 text-white px-2">›</button>
            </>
          )}
        </div>

        <div className="space-y-4">
          <span className="text-xs bg-gray-200 px-3 py-1 rounded-full text-black">{category}</span>

          <h1 className="text-2xl font-bold">{product?.name}</h1>
          <p className="text-xl">{product?.price?.toFixed(2)} AZN</p>
          <p>{product?.description}</p>
          <p>Stock: {product?.stockCount}</p>

          {isAdmin && (
            <div className="flex gap-3">
              <button onClick={() => setShowEdit(true)} className="border px-4 py-2">Edit</button>
              <button onClick={() => setShowImageModal(true)} className="border px-4 py-2">Images</button>
            </div>
          )}

          {!isAdmin && (
            <>
              <div className="flex gap-3 items-center">
                <button onClick={() => setQuantity(q => Math.max(1, q - 1))}>-</button>
                <span>{quantity}</span>
                <button onClick={() => setQuantity(q => Math.min(product?.stockCount || 0, q + 1))}
                  disabled={isOutOfStock}>+</button>
              </div>

              <button onClick={addToBasket} disabled={isOutOfStock}
                className="border px-4 py-2">
                {isOutOfStock ? "OUT OF STOCK" : added ? "✓ ADDED" : "ADD TO BASKET"}
              </button>
            </>
          )}
        </div>
      </div>

      {showEdit &&
        (<div className="fixed inset-0 bg-black/60 flex justify-center items-center">
          <div className="bg-white text-black p-6 rounded-xl w-[400px] space-y-3">
            <h2>Edit Product</h2> <input className="w-full border p-2"
              value={form.name} onChange={e => setForm({ ...form, name: e.target.value })} />
            <input className="w-full border p-2" value={form.title} onChange={e => setForm({ ...form, title: e.target.value })} />
            <input type="number" className="w-full border p-2" value={form.price} onChange={e => setForm({ ...form, price: e.target.value })} />
            <input type="number" className="w-full border p-2" value={form.stockCount} onChange={e => setForm({ ...form, stockCount: e.target.value })} />
             <textarea className="w-full border p-2" value={form.description} onChange={e => setForm({ ...form, description: e.target.value })} />
               <select className="w-full border p-2" value={form.categoryId} onChange={e => setForm({ ...form, categoryId: e.target.value })}> {categories.map(c => <option key={c.id} value={c.id}>{c.name}</option>)} </select> <div className="flex justify-end gap-2">
                 <button onClick={() => setShowEdit(false)}>Cancel</button> <button onClick={updateProduct} className="bg-black text-white px-3 py-1">Save</button> </div> </div> </div>)}
                  {showImageModal && (<div className="fixed inset-0 bg-black/60 flex justify-center items-center"> <div className="bg-white text-black p-6 rounded-xl w-[400px] space-y-4"> <h2>Manage Images</h2> <input type="file" onChange={(e) => setEditImage(e.target.files[0])} /> <button onClick={uploadImage} className="bg-black text-white px-3 py-1">Upload</button> <div className="grid grid-cols-3 gap-2"> {images.map(img => (<div key={img.id} className="relative"> <img src={img.url} className="h-20 w-full object-cover" /> <button onClick={() => deleteImage(img.id)} className="absolute top-0 right-0 bg-red-500 text-white px-1">x</button> </div>))} </div> <button onClick={() => setShowImageModal(false)}>Close</button> </div> </div>)}

      <Footer />
    </div>
  )
}

export default Details