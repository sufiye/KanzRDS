import { useState, useEffect } from "react";
import { useDarkmode } from "../stores/store";
import { useNavigate } from "react-router-dom";
import api from "../utils/axios";
import { useTokens } from "../stores/tokenStore";

const CartCard = ({ product }) => {
  const [image, setImage] = useState("/no-image.png");
  const [added, setAdded] = useState(false);

  const { isDarkmodeEnabled } = useDarkmode();
  const navigate = useNavigate();
  const { accessToken, roles } = useTokens();

  const isAdmin = roles?.includes("Admin");

  const isOutOfStock = !isAdmin && product?.stockCount === 0;

  const imageGet = async (productId) => {
    try {
      if (!productId) {
        setImage("/no-image.png");
        return;
      }

      const res = await api.get(`/Attachment/${productId}`);
      const data = res?.data;

      if (Array.isArray(data) && data.length > 0) {
        setImage(data[0]?.imgUrl || "/no-image.png");
      } else {
        setImage("/no-image.png");
      }
    } catch (error) {
      console.error("IMAGE ERROR:", error);
      setImage("/no-image.png");
    }
  };

  const goToDetails = () => {
    if (!product?.id) return;

    if (!isOutOfStock || isAdmin) {
      navigate(`/details/${product.id}`);
    }
  };

  const addToBasket = async (e) => {
    e.stopPropagation();

    if (isOutOfStock) return;

    if (!accessToken) {
      navigate("/login");
      return;
    }

    try {
      await api.post("/BasketItem", {
        productId: product?.id,
        quantity: 1,
      });

      setAdded(true);
      setTimeout(() => setAdded(false), 1500);
    } catch (error) {
      console.error("BASKET ERROR:", error);
    }
  };

  useEffect(() => {
    if (product?.id) {
      imageGet(product.id);
    }
  }, [product?.id]);

  return (
    <div
      onClick={goToDetails}
      className={`relative p-3 rounded-xl flex flex-col gap-2 border
      transition-all duration-300
      ${!isOutOfStock ? "hover:scale-[1.03] cursor-pointer" : ""}
      ${isDarkmodeEnabled ? "bg-[#1f1b16]" : "bg-[#f4efe7]"}
      ${isOutOfStock ? "opacity-50 cursor-not-allowed" : ""}
      `}
    >
      {isOutOfStock && !isAdmin && (
        <div className="absolute top-2 left-2 bg-black text-white text-[10px] px-2 py-1 rounded">
          SOLD OUT
        </div>
      )}

      <img
        className="w-full h-[200px] object-cover rounded-lg"
        src={image}
        alt={product?.name || "product"}
      />

      <h1 className="text-sm tracking-wide line-clamp-2">
        {product?.name || ""}
      </h1>

      <p className="text-sm opacity-70">
        {(product?.price ?? 0).toFixed(2)} AZN
      </p>

      {!isAdmin && (
        <button
          onClick={addToBasket}
          disabled={isOutOfStock}
          className={`relative overflow-hidden border text-xs tracking-wide py-2 mt-2 transition-all duration-300
          ${
            isDarkmodeEnabled
              ? "border-[#c2b6a3] text-[#e7dccf]"
              : "border-[#3a3835] text-[#3a3835]"
          }
          ${!added && !isOutOfStock ? "hover:bg-black hover:text-white" : ""}
          ${isOutOfStock ? "opacity-50 cursor-not-allowed" : ""}
          `}
        >
          <span className={`transition ${added ? "opacity-0" : "opacity-100"}`}>
            {isOutOfStock ? "OUT OF STOCK" : "ADD TO BASKET"}
          </span>

          <span
            className={`absolute inset-0 flex items-center justify-center transition
            ${added ? "opacity-100 scale-100" : "opacity-0 scale-75"}`}
          >
            ✓ ADDED
          </span>
        </button>
      )}
    </div>
  );
};

export default CartCard;