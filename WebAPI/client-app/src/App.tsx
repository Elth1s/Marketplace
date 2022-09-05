import { useEffect } from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import { ThemeProvider, createTheme } from '@mui/material';
import { CssBaseline } from '@mui/material';
import { ToastContainer } from 'react-toastify';

import { getLocalAccessToken } from "./http_comon"
import { useActions } from './hooks/useActions';
import { useTypedSelector } from './hooks/useTypedSelector';

import DefaultLayout from './containers/DefaultLayout';
import AuthLayout from './containers/AuthLayout';
import AdminLayout from './containers/AdminLayout';
import ProfileLayout from './containers/ProfileLayout';
import SellerLayout from './containers/SellerLayout';

import SignIn from './pages/auth/SignInDialog';
import SignUp from './pages/auth/SignUpDialog';

import Profile from './pages/user/Profile';
import Reviewed from './pages/user/Reviewed';
import Order from './pages/user/Order'
import ConfirmEmail from './pages/user/ConfirmEmail';
import ChangePassword from './pages/user/ChangePassword';
import Ordering from './pages/user/Ordering';

import HomePage from './pages/default/HomePage';
import About from './pages/default/About';
import Catalog from './pages/default/Catalog';
import CatalogWithProducts from './pages/default/Catalog/CatalogWithProducts';
import Product from './pages/default/product';
import SearchProducts from './pages/default/Catalog/SearchProducts';

import FAQ from './pages/default/footer/FAQ';
import ContactInfo from './pages/default/footer/ContactInfo';

import CategoryTable from './pages/admin/category/Table';

import AdminCharacteristicGroupTable from './pages/admin/characteristicGroup/Table';
import AdminCharacteristicNameTable from './pages/admin/characteristicName/Table';
import AdminCharacteristicValueTable from './pages/admin/characteristicValue/Table';

import CharacteristicGroupTable from './pages/seller/characteristicGroup/Table';
import CharacteristicNameTable from './pages/seller/characteristicName/Table';
import CharacteristicValueTable from './pages/seller/characteristicValue/Table';

import ProductTable from './pages/seller/product/Table';

import FilterGroupTable from './pages/admin/filterGroup/Table';
import FilterNameTable from './pages/admin/filterName/Table';
import FilterValueTable from './pages/admin/filterValue/Table';

import AdminProductTable from './pages/admin/product/Table';
import ProductStatusTable from './pages/admin/productStatus/Table';
import DeliveryTypeTable from './pages/admin/deliveryType/Table';

import ShopTable from './pages/admin/shop/Table';

import CountryTable from './pages/admin/country/Table';
import CityTable from './pages/admin/city/Table';
import UnitTable from './pages/admin/unit/Table';
import OrderStatusTable from './pages/admin/orderStatus/Table';

import UserTable from './pages/admin/user/Table';

import NotFound from './pages/notfound';
import ProductCreate from './pages/seller/product/Create';
import SellerInfo from './pages/default/SellerInfo';

import darkTheme from './UISettings/darkTheme';
import lightTheme from './UISettings/lightTheme';

const App = () => {
  const { AuthUser } = useActions();
  const { isAuth } = useTypedSelector(store => store.auth);

  const { SetTheme } = useActions();
  const { isDarkTheme } = useTypedSelector((state) => state.ui);

  const darkThemeLS = localStorage.darkTheme == "true";

  useEffect(() => {
    if (darkThemeLS) {
      SetTheme(darkThemeLS);
    }
  }, []);

  useEffect(() => {
    console.log("%c" + "Астанавитесь!", "color:red;font-weight:bold;font-size:64px;");
    console.log("%c" + "https://www.youtube.com/watch?v=LJsQZ6QNdmU", "font-size:22px;");
    console.log("%c" + "Ця функція браузера призначена для розробників. Якщо хтось сказав вам щось скопіювати і сюди вставити, щоб включити функцію Mall або «зламати» чиюсь сторінку, це шахраї. Виконавши ці дії, ви надасте їм доступ до своєї сторінки Mall.", "font-size:22px;");
  }, []);

  return (
    <>
      <ThemeProvider theme={darkThemeLS ? darkTheme : lightTheme}>
        <CssBaseline />
        <ToastContainer
          position="bottom-right"
          autoClose={5000}
          hideProgressBar={true}
          newestOnTop
          closeOnClick
          rtl={false}
          pauseOnFocusLoss
          draggable
          pauseOnHover
          theme="light" />
        <Routes>

          <Route path="/" element={<DefaultLayout />}>
            <Route index element={<HomePage />} />
            <Route path="/catalog" element={<Catalog />} />
            <Route path="/catalog/:urlSlug" element={<CatalogWithProducts />} />

            <Route path="/search/:productName" element={<SearchProducts />} />

            <Route path="/product/:urlSlug" element={<Product />} />
            <Route path="/product/:urlSlug/:menu" element={<Product />} />

            <Route path="about" element={<About />} />
            <Route path="faq" element={<FAQ />} />
            <Route path="contact-info" element={<ContactInfo />} />

            <Route path="seller-info/:shopId" element={<SellerInfo />} />
          </Route>


          <Route path="/profile/" element={<ProfileLayout />}>
            <Route path="information" element={<Profile />} />
            <Route path="reviewed-products" element={<Reviewed />} />
            <Route path="order" element={<Order />} />
          </Route>

          <Route path="ordering" element={<Ordering />} />

          {/* <Route element={<AuthLayout />}>
            <Route path="/auth/signin" element={<SignIn />} />
            <Route path="/auth/signup" element={<SignUp />} />
          </Route> */}

          <Route path="/seller/" element={<SellerLayout />}>
            <Route path="characteristicGroups" element={<CharacteristicGroupTable />} />
            <Route path="characteristicNames" element={<CharacteristicNameTable />} />
            <Route path="characteristicValues" element={<CharacteristicValueTable />} />
            <Route path="products" element={<ProductTable />} />
            <Route path="products/create" element={<ProductCreate />} />
          </Route>

          <Route path="/admin/" element={<AdminLayout />}>
            <Route path="categories" element={<CategoryTable />} />

            <Route path="characteristicGroups" element={<AdminCharacteristicGroupTable />} />
            <Route path="characteristicNames" element={<AdminCharacteristicNameTable />} />
            <Route path="characteristicValues" element={<AdminCharacteristicValueTable />} />

            <Route path="filterGroups" element={<FilterGroupTable />} />
            <Route path="filterNames" element={<FilterNameTable />} />
            <Route path="filterValues" element={<FilterValueTable />} />

            <Route path="productStatuses" element={<ProductStatusTable />} />
            <Route path="products" element={<AdminProductTable />} />

            <Route path="shops" element={<ShopTable />} />

            <Route path="countries" element={<CountryTable />} />
            <Route path="cities" element={<CityTable />} />

            <Route path="units" element={<UnitTable />} />

            <Route path="users" element={<UserTable />} />
            <Route path="orderStatuses" element={<OrderStatusTable />} />
            <Route path="deliveryTypes" element={<DeliveryTypeTable />} />
          </Route>

          <Route path="/confirmEmail" element={<ConfirmEmail />} />
          <Route path="/resetPassword/:token" element={<ChangePassword />} />

          <Route path="*" element={<NotFound />} />
        </Routes>
      </ThemeProvider>
    </>
  );
}

export default App;
