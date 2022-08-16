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
import Catalog from './pages/default/Catalog';
import CatalogWithProducts from './pages/default/Catalog/CatalogWithProducts';
import Product from './pages/default/product';

import CategoryTable from './pages/admin/category/Table';
import CategoryCreate from './pages/admin/category/Create';
import CategoryUpdate from './pages/admin/category/Update';

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

import ShopTable from './pages/admin/shop/Table';

import CountryTable from './pages/admin/country/Table';
import CityTable from './pages/admin/city/Table';
import UnitTable from './pages/admin/unit/Table';
import OrderStatusTable from './pages/admin/orderStatus/Table';

import UserTable from './pages/admin/user/Table';

import NotFound from './pages/notfound';
import ProductCreate from './pages/seller/product/Create';
import SellerHome from './pages/seller/Home';

function App() {
  const { isAuth } = useTypedSelector(store => store.auth);
  const { AuthUser } = useActions();

  useEffect(() => {
    console.log("%c" + "Астанавитесь!", "color:red;font-weight:bold;font-size:64px;");
    console.log("%c" + "https://www.youtube.com/watch?v=LJsQZ6QNdmU", "font-size:22px;");
    console.log("%c" + "Ця функція браузера призначена для розробників. Якщо хтось сказав вам щось скопіювати і сюди вставити, щоб включити функцію Mall або «зламати» чиюсь сторінку, це шахраї. Виконавши ці дії, ви надасте їм доступ до своєї сторінки Mall.", "font-size:22px;");
  }, []);

  const theme = createTheme({
    breakpoints: {
      values: {
        xs: 0,
        sm: 600,
        md: 900,
        lg: 1185,
        xl: 1560,
      },
    },
    palette: {
      mode: "light",
      primary: {
        main: '#F45626',
        dark: "#CB2525"
      },
      secondary: {
        main: '#0E7C3A',
        light: "#30AA61"
      },
      error: {
        main: '#AF0000',
      },
      common: {
        black: "#000",
        white: "#fff"
      }
    },
    components: {
      MuiContainer: {
        defaultProps: {
          style: {
            padding: 0
          }
        }
      },
      MuiIconButton: {
        defaultProps: {
          color: "secondary",
        },
        styleOverrides: {
          root: {
            "&& .MuiTouchRipple-child": { borderRadius: "12px" }
          }
        }
      },
      MuiTypography: {
        defaultProps: {
          color: "#000"
        },
      },
      MuiSwitch: {
        styleOverrides: {
          switchBase: {
            color: "#0E7C3A"
          },
        }
      },
      MuiAppBar: {
        defaultProps: {
          color: "inherit"
        }
      }
    },
    typography: {
      h1: {
        fontSize: "36px",
        lineHeight: "45px"
      },
      h2: {
        fontSize: "27px",
        lineHeight: "34px"
      },
      h3: {
        fontSize: "24px",
        lineHeight: "27px"
      },
      h4: {
        fontSize: "20px",
        lineHeight: "25px"
      },
      h5: {
        fontSize: "18px",
        lineHeight: "23px"
      },
      h6: {
        fontSize: "16px",
        lineHeight: "20px"
      },
      subtitle1: {
        fontSize: "14px",
        lineHeight: "18px"
      },
      fontFamily: [
        'Mulish',
        "sans-serif"
      ].join(',')
    },
  });

  return (
    <>
      <ThemeProvider theme={theme}>
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
            <Route path="/product/:urlSlug" element={<Product />} />
            <Route path="/product/:urlSlug/:menu" element={<Product />} />
          </Route>

          <Route path="/seller" element={<SellerLayout />}>
            <Route index element={<SellerHome />} />
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
            <Route path="categories/create" element={<CategoryCreate />} />
            <Route path="categories/update/:id" element={<CategoryUpdate />} />

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
