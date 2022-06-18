import { useEffect } from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import { CssBaseline } from '@mui/material';

import { getLocalAccessToken } from "./http_comon"
import { useActions } from './hooks/useActions';
import { useTypedSelector } from './hooks/useTypedSelector';

import AuthLayout from './containers/AuthLayout';
import AdminLayout from './containers/AdminLayout';

import SignIn from './pages/auth/SignIn';
import SignUp from './pages/auth/SignUp';
import Profile from './pages/user/Profile';
import ConfirmEmail from './pages/user/ConfirmEmail';
import ResetPassword from './pages/user/ForgotPassword/Reset';
import ChangePassword from './pages/user/ForgotPassword/Change';

import CategoryTable from './pages/admin/category/Table';
import CategoryCreate from './pages/admin/category/Create';
import CategoryUpdate from './pages/admin/category/Update';

import CharacteristicGroupTable from './pages/admin/characteristicGroup/Table';
import CharacteristicTable from './pages/admin/characteristic/Table';

function App() {
  const { isAuth } = useTypedSelector(store => store.auth);
  const { AuthUser } = useActions();

  useEffect(() => {
    let token = getLocalAccessToken();
    if (token) {
      AuthUser(token);
    }
  }, []);

  const theme = createTheme({
    palette: {
      primary: {
        main: '#F45626',
      },
      secondary: {
        main: '#0E7C3A',
      },
      error: {
        main: '#AF0000',
      },
    },
    typography: {
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

        <Route element={<AuthLayout />}>
          <Route path="/auth/signin" element={<SignIn />} />
          <Route path="/auth/signup" element={<SignUp />} />
        </Route>

        <Route path='/admin' element={<AdminLayout />}>

        </Route>

        <Route path="/profile" element={<Profile />} />
        <Route path="/confirmEmail" element={<ConfirmEmail />} />
        <Route path="/resetPassword" element={<ResetPassword />} />
        <Route path="/resetPassword/:token" element={<ChangePassword />} />



        <Route path="/category" element={<CategoryTable />} />
        <Route path="/category/create" element={<CategoryCreate />} />
        <Route path="/category/update" element={<CategoryUpdate />} />

        <Route path="/CharacteristicGroup" element={<CharacteristicGroupTable />} />
        <Route path="/characteristic" element={<CharacteristicTable />} />

        {/* <Route path="*" element={<NotFound />} /> */}
      </Routes>
    </ThemeProvider>
  );
}

export default App;
