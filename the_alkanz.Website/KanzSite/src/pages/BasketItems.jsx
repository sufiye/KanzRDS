import Navbar from "../components/Navbar";
import Footer from "../components/Footer";
import { useState, useEffect } from "react";
import { useDarkmode } from "../stores/store";
import { useTokens } from "../stores/tokenStore";
import api from "../utils/axios";
import toast from "react-hot-toast";
import { useNavigate } from "react-router-dom";

const BasketItems = () => {
  const { isDarkmodeEnabled } = useDarkmode();
  const { accessToken } = useTokens();
  const navigate = useNavigate();

  const [basketItems, setBasketItems] = useState([]);
  const [loading, setLoading] = useState(false);

  // ✅ SAFE NORMALIZER
  const normalizeArray = (data) => {
    if (Array.isArray(data)) return data;
    if (Array.isArray(data?.items)) return data.items;
    if (Array.isArray(data?.data)) return data.data;
    return [];
  };

  // ✅ FIXED IMAGE FETCH (return always)
  const fetchImage = async (productId) => {
    try {
      const res = await api.get(`/Attachment/${productId}`);

      const data = normalizeArray(res.data);

      if (data.length > 0) {
        return data[0].imgUrl;
      }

      return "/no-image.png";
    } catch (error) {
      console.error("IMAGE ERROR:", error);
      return "/no-image.png";
    }
  };

  const getBasketItems = async () => {
    if (!accessToken) return;

    try {
      const res = await api.get("/BasketItem", {
        headers: { Authorization: `Bearer ${accessToken}` },
      });

      const data = normalizeArray(res.data);

      const itemsWithImages = await Promise.all(
        data.map(async (item) => {
          let imageUrl = "/no-image.png";

          const productId = item?.product?.id;

          if (productId) {
            try {
              imageUrl = await fetchImage(productId);
            } catch (err) {
              console.error("IMAGE FETCH ERROR:", err);
              imageUrl = "/no-image.png";
            }
          }

          return {
            ...item,
            imageUrl,
          };
        })
      );

      setBasketItems(itemsWithImages);
    } catch (error) {
      console.error("BASKET ERROR:", error);
      toast.error("Failed to load basket");
      setBasketItems([]);
    }
  };

  const removeFromBasket = async (id) => {
    if (!accessToken) return;

    try {
      await api.delete(`/BasketItem/${id}`, {
        headers: { Authorization: `Bearer ${accessToken}` },
      });

      setBasketItems((prev) => prev.filter((item) => item.id !== id));
      toast.success("Item removed");
    } catch {
      toast.error("Failed to remove");
    }
  };

  const totalPrice = basketItems.reduce(
    (sum, item) =>
      sum + (item.product?.price || 0) * (item.quantity || 1),
    0
  );

  const makeOrder = async () => {
    if (!accessToken || loading) return;

    setLoading(true);

    try {
      await api.post("/Order", {}, {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      });

      toast.success("Order placed successfully ✅");

      setBasketItems([]);

      setTimeout(() => {
        navigate("/orders");
      }, 800);

    } catch (error) {
      console.error("ORDER ERROR:", error);
      toast.error("Failed to place order ❌");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (!accessToken) return;
    getBasketItems();
  }, [accessToken]);

  return (
    <div className={`w-full min-h-screen transition
    ${isDarkmodeEnabled
        ? "bg-[#1c1814] text-[#e6dccf]"
        : "bg-[#f4efe7] text-[#3a3835]"
      }`}>

      <Navbar />

      <div className="max-w-7xl mx-auto py-20 px-6">

        <h1 className="text-2xl tracking-[3px] mb-10">
          MY BASKET
        </h1>

        {basketItems.length === 0 ? (
          <p className="opacity-70">Your basket is empty.</p>
        ) : (
          <div className="grid lg:grid-cols-3 gap-10">

            <div className="lg:col-span-2 space-y-6">

              {Array.isArray(basketItems) &&
                basketItems.map((item) => (
                  <div
                    key={item.id}
                    className={`flex gap-6 p-5 rounded-xl border transition
                  ${isDarkmodeEnabled
                        ? "border-[#3a342c] bg-[#26221d]"
                        : "border-[#e5e0d8] bg-white"
                      }`}
                  >

                    <img
                      src={item.imageUrl}
                      className="w-28 h-28 object-cover rounded-lg"
                    />

                    <div className="flex flex-col justify-between flex-1">

                      <div>
                        <h2 className="text-base font-semibold mb-1">
                          {item.product?.name}
                        </h2>
                        <p className="text-xs opacity-70 line-clamp-2">
                          {item.product?.description}
                        </p>
                      </div>

                      <div className="flex justify-between items-center mt-4">

                        <div className="text-sm">
                          {item.product?.price?.toFixed(2)} AZN × {item.quantity}
                        </div>

                        <button
                          onClick={() => removeFromBasket(item.id)}
                          className="text-xs border px-4 py-2 hover:bg-black hover:text-white transition"
                        >
                          REMOVE
                        </button>

                      </div>

                    </div>

                  </div>
                ))}

            </div>

            <div className={`h-fit p-6 rounded-xl border
              ${isDarkmodeEnabled
                ? "border-[#3a342c] bg-[#26221d]"
                : "border-[#e5e0d8] bg-white"
              }`}>

              <h2 className="text-lg mb-6 tracking-wide">
                ORDER SUMMARY
              </h2>

              <div className="flex justify-between mb-4 text-sm">
                <span>Total</span>
                <span>{totalPrice.toFixed(2)} AZN</span>
              </div>

              <button
                onClick={makeOrder}
                disabled={loading}
                className={`w-full border py-3 text-sm tracking-wide transition
                ${loading
                    ? "opacity-50 cursor-not-allowed"
                    : "hover:bg-black hover:text-white"
                  }`}
              >
                {loading ? "Processing..." : "PLACE ORDER"}
              </button>

            </div>

          </div>
        )}

      </div>

      <Footer />
    </div>
  );
};

export default BasketItems;