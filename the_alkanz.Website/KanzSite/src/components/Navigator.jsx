import { Route, Routes } from "react-router-dom"
import HomePage from "../pages/HomePage"
import NotFound from "../pages/NotFound"
import Login from "../pages/Login"
import Register from "../pages/Register"
import Details from "../pages/Details"
import BasketItems from "../pages/BasketItems"
import Order from "../pages/Order"


const Navigator = () => {
  return (
   <Routes>
    <Route path="/" element={<HomePage/>} />
    <Route path="/category/:category" element={<HomePage/>} />
    <Route path="/login" element={<Login/>} />
    <Route path="/register" element={<Register/>} />
    <Route path="/basketItems" element={<BasketItems/>} />
    <Route path="/basketItems/:id" element={<BasketItems/>} />
    <Route path="/orders" element={<Order/>} />
    <Route path="/details/:id" element={<Details/>} />
    <Route path="*" element={<NotFound/>} />

   </Routes>
  )
}

export default Navigator