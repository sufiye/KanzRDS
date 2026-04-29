import { useDarkmode } from "../stores/store"

const Footer = () => {
  const { isDarkmodeEnabled } = useDarkmode()

  return (
    <div
      className={`w-full mt-20 px-10 py-16 transition
      ${isDarkmodeEnabled
          ? "bg-[#1c1814] text-[#e6dccf]"
          : "bg-[#f4efe7] text-[#3a3835]"
        }`}
    >
      <div className="max-w-7xl mx-auto grid md:grid-cols-3 gap-10">

        <div className="space-y-4">
          <h1 className="text-2xl font-semibold tracking-[3px]">
            Kanz
          </h1>

          <p className="text-sm opacity-70 leading-6">
            Handcrafted aesthetic products for girls.
            Makeup, bags, candles and more — designed with love and elegance.
          </p>

          <p className="text-xs opacity-50">
            Since 2026 • Created by Meryem & Sufiye
          </p>
        </div>

        <div className="space-y-3 text-sm">
          <h2 className="font-semibold tracking-wide mb-2">
            Quick Links
          </h2>

          <p className="hover:opacity-60 cursor-pointer transition">
            Home
          </p>
          <p className="hover:opacity-60 cursor-pointer transition">
            Basket
          </p>
          <p className="hover:opacity-60 cursor-pointer transition">
            Orders
          </p>

        </div>

        <div className="space-y-4">
          <h2 className="font-semibold tracking-wide">
            Follow Us
          </h2>

          <a
            href="https://www.instagram.com/the_alkanz?igsh=empweXhjb3Boc3Rs&utm_source=qr"
            target="_blank"
            className="flex items-center gap-3 hover:opacity-70 transition"
          >
            <div className="w-10 h-10 flex items-center justify-center rounded-full overflow-hidden border">
              <img
                className="w-full h-full object-cover rounded-full"
                src="/src/assets/kanzlogo.jpg"
                alt="kanzLogo"
              />
            </div>
            <span className="text-sm">@the_alkanz</span>
          </a>

          <p className="text-sm opacity-70">
            Follow us for new collections, aesthetic drops and inspiration ✨
          </p>
        </div>

      </div>

      <div className="mt-12 pt-6 border-t border-[#cbbfae]/30 text-center text-xs opacity-60">
        © 2026 Kanz. All rights reserved.
      </div>
    </div>
  )
}

export default Footer