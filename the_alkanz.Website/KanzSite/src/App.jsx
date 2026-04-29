
import Navigator from "./components/Navigator"
import { Toaster } from "react-hot-toast"
export const App = () => {
  return (
    <>
   <div className="w-full h-screen">
    <Toaster position="top-center" />
    <Navigator/>
   </div>
   
   </>
  )
}
