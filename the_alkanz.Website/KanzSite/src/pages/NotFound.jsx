import { useEffect } from "react"
import { useNavigate } from "react-router-dom"
import { useDarkmode } from "../stores/store"

const NotFound = () => {
  const { isDarkmodeEnabled } = useDarkmode()
  const navigate = useNavigate()

  useEffect(() => {
    const timer = setTimeout(() => {
      navigate("/")
    }, 3000)

    return () => clearTimeout(timer)
  }, [])

  return (
    <div
      className={`w-full h-screen flex flex-col justify-center items-center text-center px-5
      ${isDarkmodeEnabled
        ? "bg-[#1c1814] text-[#e6dccf]"
        : "bg-[#f4efe7] text-[#3a3835]"
      }`}
    >

      <h1 className="text-[120px] md:text-[160px] font-bold tracking-[10px] opacity-80">
        404
      </h1>

      <p className="text-sm tracking-[4px] mb-4">
        PAGE NOT FOUND
      </p>

      <p className="text-xs opacity-70 mb-8 max-w-md">
        The page you are looking for doesn’t exist or has been moved.
      </p>

      <button
        onClick={() => navigate("/")}
        className="border px-6 py-2 text-xs tracking-wide
        transition-all duration-300
        hover:bg-black hover:text-white active:scale-95"
      >
        BACK TO HOME
      </button>

      <p className="text-[10px] opacity-50 mt-6">
        Redirecting in 3 seconds...
      </p>

    </div>
  )
}

export default NotFound