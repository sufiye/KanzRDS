import NavbarLr from "../components/NavbarLR"
import Footer from "../components/Footer"
import { Link, useNavigate } from "react-router-dom"
import { useTokens } from "../stores/tokenStore"
import { useState } from "react"
import toast from "react-hot-toast"
import { useDarkmode } from "../stores/store"
import api from "../utils/axios"

const Register = () => {
  const { isDarkmodeEnabled } = useDarkmode()
  const navigate = useNavigate()
  const { setAccessToken, setRefreshToken } = useTokens()

  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    address: "",
    phoneNumber: "",
    email: "",
    password: "",
    confirmedPassword: ""
  })

  const handleInputChange = (title, value) => {
    setFormData(prev => ({ ...prev, [title]: value }))
  }

  const handleRegister = async () => {
    try {
      const res = await api.post("/Auth/register", formData)

      setAccessToken(res.data.accessToken)
      setRefreshToken(res.data.refreshToken)

      toast.success("Account created successfully 🎉")
      navigate("/")
    } catch (error) {
      console.error(error)
      toast.error("Register failed")
    }
  }

  return (
    <div className={`w-full min-h-screen transition-all
    ${isDarkmodeEnabled
        ? "bg-[#1c1814] text-[#e6dccf]"
        : "bg-[#f4efe7] text-[#3a3835]"
      }`}>

      <NavbarLr />

      <div className="flex justify-center items-center my-20 px-4">

        <div className={`w-full max-w-md p-10 rounded-2xl shadow-sm
        ${isDarkmodeEnabled ? "bg-[#26221d]" : "bg-white"}`}>

          <h1 className="text-2xl text-center mb-8 tracking-[3px]">
            REGISTER
          </h1>

          <div className="space-y-3">

            <input
              value={formData.firstName}
              onChange={e => handleInputChange("firstName", e.target.value)}
              className={`w-full p-3 border rounded-lg text-sm
              ${isDarkmodeEnabled
                  ? "bg-[#1c1814] border-[#3a342c] text-white placeholder:text-[#a8a093]"
                  : "bg-[#f9f6f1] border-[#ddd] text-black placeholder:text-gray-500"
                }`}
              placeholder="First Name"
            />

            <input
              value={formData.lastName}
              onChange={e => handleInputChange("lastName", e.target.value)}
              className={`w-full p-3 border rounded-lg text-sm
              ${isDarkmodeEnabled
                  ? "bg-[#1c1814] border-[#3a342c] text-white placeholder:text-[#a8a093]"
                  : "bg-[#f9f6f1] border-[#ddd] text-black placeholder:text-gray-500"
                }`}
              placeholder="Last Name"
            />

            <input
              value={formData.address}
              onChange={e => handleInputChange("address", e.target.value)}
              className={`w-full p-3 border rounded-lg text-sm
              ${isDarkmodeEnabled
                  ? "bg-[#1c1814] border-[#3a342c] text-white placeholder:text-[#a8a093]"
                  : "bg-[#f9f6f1] border-[#ddd] text-black placeholder:text-gray-500"
                }`}
              placeholder="Address"
            />

            <input
              value={formData.phoneNumber}
              onChange={e => handleInputChange("phoneNumber", e.target.value)}
              className={`w-full p-3 border rounded-lg text-sm
              ${isDarkmodeEnabled
                  ? "bg-[#1c1814] border-[#3a342c] text-white placeholder:text-[#a8a093]"
                  : "bg-[#f9f6f1] border-[#ddd] text-black placeholder:text-gray-500"
                }`}
              placeholder="Phone Number"
            />

            <input
              value={formData.email}
              onChange={e => handleInputChange("email", e.target.value)}
              className={`w-full p-3 border rounded-lg text-sm
              ${isDarkmodeEnabled
                  ? "bg-[#1c1814] border-[#3a342c] text-white placeholder:text-[#a8a093]"
                  : "bg-[#f9f6f1] border-[#ddd] text-black placeholder:text-gray-500"
                }`}
              placeholder="Email"
              type="email"
            />

            <input
              value={formData.password}
              onChange={e => handleInputChange("password", e.target.value)}
              className={`w-full p-3 border rounded-lg text-sm
              ${isDarkmodeEnabled
                  ? "bg-[#1c1814] border-[#3a342c] text-white placeholder:text-[#a8a093]"
                  : "bg-[#f9f6f1] border-[#ddd] text-black placeholder:text-gray-500"
                }`}
              placeholder="Password"
              type="password"
            />

            <input
              value={formData.confirmedPassword}
              onChange={e => handleInputChange("confirmedPassword", e.target.value)}
              className={`w-full p-3 border rounded-lg text-sm
              ${isDarkmodeEnabled
                  ? "bg-[#1c1814] border-[#3a342c] text-white placeholder:text-[#a8a093]"
                  : "bg-[#f9f6f1] border-[#ddd] text-black placeholder:text-gray-500"
                }`}
              placeholder="Confirm Password"
              type="password"
            />

          </div>

          <Link to="/login">
            <p className="text-xs mt-5 mb-6 opacity-70 hover:opacity-100 cursor-pointer">
              Already have an account?
            </p>
          </Link>

          <button
            onClick={handleRegister}
            className="w-full border py-3 text-sm tracking-wide transition
            hover:bg-black hover:text-white"
          >
            REGISTER
          </button>

        </div>
      </div>

      <Footer />
    </div>
  )
}

export default Register