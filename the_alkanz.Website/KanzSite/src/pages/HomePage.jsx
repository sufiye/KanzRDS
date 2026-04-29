import Navbar from "../components/Navbar";
import Footer from "../components/Footer";
import Card from "../components/Card";
import { useState, useEffect } from "react";
import { useDarkmode } from "../stores/store";
import api from "../utils/axios";
import { useTokens } from "../stores/tokenStore";

const HomePage = () => {
  const [products, setProducts] = useState([]);
  const [allLoaded, setAllLoaded] = useState(false);
  const [page, setPage] = useState(1);

  const [searchTerm, setSearchTerm] = useState("");
  const [selectedCategory, setSelectedCategory] = useState("");

  const [showModal, setShowModal] = useState(false);
  const [showCategoryModal, setShowCategoryModal] = useState(false);

  const [categories, setCategories] = useState([]);
  const [newCategory, setNewCategory] = useState("");

  const [form, setForm] = useState({
    name: "",
    title: "",
    description: "",
    categoryId: "",
    price: 0,
    stockCount: 0,
  });

  const roles = useTokens((state) => state.roles);
  const isAdmin = roles.includes("Admin");

  const { isDarkmodeEnabled } = useDarkmode();

  const getProducts = async () => {
    try {
      let res;

      if (selectedCategory) {
        res = await api.get(`/Product/${selectedCategory}/category`);

        let filtered = res.data;

        if (searchTerm.length >= 1) {
          filtered = filtered.filter(p =>
            p.name.toLowerCase().includes(searchTerm.toLowerCase())
          );
        }

        setProducts(filtered);
        setAllLoaded(true);
        return;
      }

      let url = `/Product/pagedResult?page=1&pageSize=10`;

      if (searchTerm.length >= 1) {
        url += `&search=${searchTerm}`;
      }

      res = await api.get(url);

      setProducts(res.data.items);
      setPage(1);
      setAllLoaded((res.data.items || []).length < 10);

    } catch (error) {
      console.error(error);
    }
  };

  const loadMore = async () => {
    if (selectedCategory) return;

    const nextPage = page + 1;

    let url = `/Product/pagedResult?page=${nextPage}&pageSize=10`;

    if (searchTerm.length >= 1) {
      url += `&search=${searchTerm}`;
    }

    const res = await api.get(url);

    setProducts(prev => [...prev, ...(res.data.items || [])]);
    setPage(nextPage);

    if (!res.data.items || res.data.items.length < 10) {
      setAllLoaded(true);
    }
  };

  const showLess = () => {
    setProducts(products.slice(0, 10));
    setPage(1);
    setAllLoaded(false);
  };

  const getCategories = async () => {
    const res = await api.get("/Category");
    setCategories(res.data);
  };

const addProduct = async () => {
  try {

    if (
      !form.name.trim() ||
      !form.title.trim() ||
      !form.description.trim() ||
      !form.categoryId ||
      !form.price ||
      !form.stockCount
    ) {
      alert("All fields are required!");
      return;
    }

    const formData = new FormData();

    formData.append("name", form.name);
    formData.append("title", form.title);
    formData.append("description", form.description);
    formData.append("categoryId", form.categoryId);
    formData.append("price", form.price);
    formData.append("stockCount", form.stockCount);

    await api.post("/Product", formData);

    setShowModal(false);

    setForm({
      name: "",
      title: "",
      description: "",
      categoryId: "",
      price: 0,
      stockCount: 0,
    });

    getProducts();

  } catch (err) {
    console.error("ADD PRODUCT ERROR:", err);
  }
};

  const deleteProduct = async (product) => {
    try {
      if (product.attachments && product.attachments.length > 0) {
        await Promise.all(
          product.attachments.map(att =>
            api.delete(`/Attachment/${att.id}`)
          )
        );
      }

      await api.delete(`/Product/${product.id}`);

      setProducts(prev => prev.filter(p => p.id !== product.id));

    } catch (err) {
      console.error("DELETE ERROR:", err);
    }
  };

  const addCategory = async () => {
    try {

      if (!newCategory.trim()) {
        alert("Category name cannot be empty!");
        return;
      }
       const exists = categories.some(
      (cat) => cat.name.toLowerCase() === newCategory.trim().toLowerCase()
    );

    if (exists) {
      alert("Category name already exists!");
      return;
    }

      await api.post("/Category", { name: newCategory });

      setNewCategory("");
      getCategories();

    } catch (err) {
      console.error("ADD CATEGORY ERROR:", err);
    }
  };

  const deleteCategory = async (id) => {
    try {
      await api.delete(`/Category/${id}`);
      setCategories(prev => prev.filter(c => c.id !== id));
    } catch (err) {
      console.error("DELETE CATEGORY ERROR:", err);
    }
  };

  useEffect(() => {
    getProducts();
  }, [searchTerm, selectedCategory]);

  useEffect(() => {
    getCategories();
  }, []);

  return (
    <div className={`min-h-screen ${isDarkmodeEnabled
      ? "bg-[#1c1814] text-[#e6dccf]"
      : "bg-[#f4efe7] text-[#3a3835]"
      }`}>

      <Navbar
        searchterm={searchTerm}
        setSearchterm={setSearchTerm}
        selectedCategory={selectedCategory}
        setSelectedCategory={setSelectedCategory}
      />

      <h2 className="text-center mt-16 sm:mt-20 mb-8 sm:mb-10 tracking-[3px] text-xs sm:text-sm">
        BACK IN STOCK
      </h2>

      {isAdmin && (
        <div className="flex flex-col sm:flex-row justify-center gap-3 sm:gap-4 mb-6 px-4">
          <button
            onClick={() => setShowModal(true)}
            className="border px-4 sm:px-6 py-2 text-xs hover:bg-black hover:text-white"
          >
            ADD PRODUCT
          </button>

          <button
            onClick={() => setShowCategoryModal(true)}
            className="border px-4 sm:px-6 py-2 text-xs hover:bg-black hover:text-white"
          >
            MANAGE CATEGORY
          </button>
        </div>
      )}

      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6 px-4 sm:px-8 lg:px-12 max-w-7xl mx-auto">
        {products.map(product => (
          <div key={product.id}>
            <Card product={product} />

            {isAdmin && (
              <button
                onClick={() => deleteProduct(product)}
                className="text-red-500 text-xs mt-2"
              >
                DELETE
              </button>
            )}
          </div>
        ))}
      </div>

      {!allLoaded && !selectedCategory && (
        <div className="flex justify-center mt-10">
          <button
            onClick={loadMore}
            className="border px-4 sm:px-6 py-2 text-sm tracking-wide transition-all duration-300 hover:bg-black hover:text-white active:scale-95"
          >
            Load More
          </button>
        </div>
      )}

      {allLoaded && products.length > 10 && (
        <div className="flex justify-center mt-4">
          <button
            onClick={showLess}
            className="border px-4 sm:px-6 py-2 text-sm tracking-wide transition-all duration-300 hover:bg-black hover:text-white active:scale-95"
          >
            Show Less
          </button>
        </div>
      )}

      {showModal && (
        <div className="fixed inset-0 bg-black/60 flex justify-center items-center px-4">
          <div className="bg-white w-full max-w-[500px] p-6 rounded-2xl text-black space-y-5">
            <h2 className="text-xl font-semibold">Add Product</h2>

            <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">

              <div>
                <label className="text-xs">Name</label>
                <input className="border p-2 rounded w-full"
                  value={form.name}
                  onChange={(e) => setForm({ ...form, name: e.target.value })}
                />
              </div>

              <div>
                <label className="text-xs">Title</label>
                <input className="border p-2 rounded w-full"
                  value={form.title}
                  onChange={(e) => setForm({ ...form, title: e.target.value })}
                />
              </div>

              <div className="sm:col-span-2">
                <label className="text-xs">Description</label>
                <textarea className="border p-2 rounded w-full"
                  value={form.description}
                  onChange={(e) => setForm({ ...form, description: e.target.value })}
                />
              </div>

              <div>
                <label className="text-xs">Category</label>
                <select className="border p-2 rounded w-full"
                  value={form.categoryId}
                  onChange={(e) => setForm({ ...form, categoryId: e.target.value })}
                >
                  <option value="">Select category</option>
                  {categories.map(cat => (
                    <option key={cat.id} value={cat.id}>{cat.name}</option>
                  ))}
                </select>
              </div>

              <div>
                <label className="text-xs">Price (AZN)</label>
                <input type="number" className="border p-2 rounded w-full"
                  value={form.price}
                  min={1}
                  onChange={(e) => setForm({ ...form, price: e.target.value })}
                />
              </div>

              <div>
                <label className="text-xs">Stock Count</label>
                <input type="number" className="border p-2 rounded w-full"
                  value={form.stockCount}
                  min={1}
                  onChange={(e) => setForm({ ...form, stockCount: e.target.value })}
                />
              </div>

            </div>

            <div className="flex flex-col sm:flex-row justify-end gap-3">
              <button onClick={() => setShowModal(false)} className="border px-4 py-2">Cancel</button>
              <button onClick={addProduct} className="bg-black text-white px-4 py-2">Add</button>
            </div>

          </div>
        </div>
      )}

      {showCategoryModal && (
        <div className="fixed inset-0 bg-black/60 flex justify-center items-center px-4">
          <div className="bg-white p-6 w-full max-w-[400px] rounded-2xl text-black space-y-4">

            <h2 className="font-semibold">Manage Categories</h2>

            <div className="flex gap-2">
              <input
                placeholder="New category"
                value={newCategory}
                onChange={(e) => setNewCategory(e.target.value)}
                className="border p-2 w-full"
              />

              <button onClick={addCategory} className="bg-green-500 text-white px-4">
                Add
              </button>
            </div>

            <div className="space-y-2 max-h-[200px] overflow-y-auto">
              {categories.map(c => (
                <div key={c.id} className="flex justify-between items-center border p-2 rounded">
                  <span>{c.name}</span>

                  <button
                    onClick={() => deleteCategory(c.id)}
                    className="text-red-500 text-sm"
                  >
                    Delete
                  </button>
                </div>
              ))}
            </div>

            <button
              onClick={() => setShowCategoryModal(false)}
              className="w-full border py-2"
            >
              Close
            </button>

          </div>
        </div> 
      )}

      <Footer />
    </div>
  );
};

export default HomePage;