import { Link } from "react-router-dom"
import { useDarkmode } from "../stores/store"

const Navbar = () => {
  const { toggleDarkmode, isDarkmodeEnabled } = useDarkmode() || {}

  return (
    <div
      className={`w-full flex justify-between items-center px-12 py-6 text-sm transition
      ${
        isDarkmodeEnabled
          ? "bg-[#1c1814] text-[#e6dccf]"
          : "bg-[#f4efe7] text-[#3a3835]"
      }`}
    >

      <h1 className="text-xl tracking-[3px] font-semibold">
        Kanz
      </h1>

      <div className="flex items-center gap-10 tracking-wide">
        <Link className="hover:opacity-60 transition" to="/">
          Home
        </Link>
      </div>

      <div className="flex items-center gap-5">

        <button
          onClick={() => toggleDarkmode?.()}
          className="opacity-70 hover:opacity-100 transition"
        >
          {isDarkmodeEnabled ? "☀️" : "🌙"}
        </button>

      </div>

    </div>
  )
}

export default Navbar