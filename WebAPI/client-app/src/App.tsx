import { useEffect } from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import { ThemeProvider, createTheme } from '@mui/material';
import { CssBaseline } from '@mui/material';

import { getLocalAccessToken } from "./http_comon"
import { useActions } from './hooks/useActions';
import { useTypedSelector } from './hooks/useTypedSelector';

import DefaultLayout from './containers/DefaultLayout';
import AuthLayout from './containers/AuthLayout';
import AdminLayout from './containers/AdminLayout';
import ProfileLayout from './containers/ProfileLayout';

import SignIn from './pages/auth/SignIn';
import SignUp from './pages/auth/SignUp';
import Profile from './pages/user/Profile';
import ConfirmEmail from './pages/user/ConfirmEmail';
import ChangePassword from './pages/user/ForgotPassword/Change';
import SentResetPasswordEmail from './pages/user/ForgotPassword/SendResetEmail';

import Product from './pages/default/product';

import CategoryTable from './pages/admin/category/Table';
import CategoryCreate from './pages/admin/category/Create';
import CategoryUpdate from './pages/admin/category/Update';

import CharacteristicGroupTable from './pages/admin/characteristicGroup/Table';
import CharacteristicNameTable from './pages/admin/characteristicName/Table';
import CountryTable from './pages/admin/country/Table';
import CityTable from './pages/admin/city/Table';
import UnitTable from './pages/admin/unit/Table';

import SendResetPasswordEmail from './pages/user/ForgotPassword/SendResetEmail';
import SendResetPasswordPhone from './pages/user/ForgotPassword/SendResetPhone';

import HomePage from './pages/default/HomePage';

import Ordering from './pages/user/Ordering';
import Catalog from './pages/default/Catalog';
import CatalogWithProducts from './pages/default/Catalog/CatalogWithProducts';

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
        lg: 1200,
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
          color: "secondary"
        },
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
        fontSize: "36px"
      },
      h2: {
        fontSize: "27px"
      },
      h3: {
        fontSize: "24px"
      },
      h4: {
        fontSize: "20px"
      },
      h5: {
        fontSize: "18px"
      },
      h6: {
        fontSize: "16px"
      },
      subtitle1: {
        fontSize: "14px"
      },
      fontFamily: [
        'Mulish',
        "sans-serif"
      ].join(',')
    },
  });

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Routes>

        <Route path="/" element={<DefaultLayout />}>
          <Route index element={<HomePage />} />
          <Route path="/catalog" element={<Catalog />} />
          <Route path="/catalog/:urlSlug" element={<CatalogWithProducts />} />
          <Route path="/product/:urlSlug" element={<Product />} />

        </Route>

        <Route path="/profile" element={<ProfileLayout />}>
          <Route path="/profile/information" element={<Profile />} />
        </Route>

        <Route path="/ordering" element={<Ordering />} />

        <Route element={<AuthLayout />}>
          <Route path="/auth/signin" element={<SignIn />} />
          <Route path="/auth/signup" element={<SignUp />} />
        </Route>

        <Route path="/admin" element={<AdminLayout />}>
          <Route path="/admin/category" element={<CategoryTable />} />
          <Route path="/admin/category/create" element={<CategoryCreate />} />
          <Route path="/admin/category/update/:id" element={<CategoryUpdate />} />

          <Route path="/admin/characteristicGroup" element={<CharacteristicGroupTable />} />
          <Route path="/admin/characteristicName" element={<CharacteristicNameTable />} />

          <Route path="/admin/country" element={<CountryTable />} />
          <Route path="/admin/city" element={<CityTable />} />

          <Route path="/admin/unit" element={<UnitTable />} />
        </Route>

        <Route path="/confirmEmail" element={<ConfirmEmail />} />
        <Route path="/resetPasswordEmail" element={<SendResetPasswordEmail />} />
        <Route path="/resetPasswordPhone" element={<SendResetPasswordPhone />} />
        <Route path="/resetPassword/:token" element={<ChangePassword />} />



        {/* <Route path="*" element={<NotFound />} /> */}
      </Routes>
    </ThemeProvider>
  );
}

export default App;
