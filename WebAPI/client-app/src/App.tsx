import { useEffect } from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import { CssBaseline } from '@mui/material';

import { getLocalAccessToken } from "./http_comon"
import { useActions } from './hooks/useActions';
import AuthLayout from './containers/AuthLayout';

import SignIn from './pages/auth/SignIn';
import SignUp from './pages/auth/SignUp';
import Profile from './pages/user/Profile';
import ConfirmEmail from './pages/user/ConfirmEmail';

import CategoryTable from './pages/admin/category/Table';
import CategoryCreate from './pages/admin/category/Create';
import CategoryUpdate from './pages/admin/category/Update';

import CharacteristicGroupTable from './pages/admin/characteristicGroup/Table';
import CharacteristicGroupCreate from './pages/admin/characteristicGroup/Create';
import CharacteristicGroupUpdate from './pages/admin/characteristicGroup/Update';

import CharacteristicTable from './pages/admin/characteristic/Table';
import CharacteristicCreate from './pages/admin/characteristic/Create';
import CharacteristicUpdate from './pages/admin/characteristic/Update';
import { useTypedSelector } from './hooks/useTypedSelector';
import ResetPassword from './pages/user/ForgotPassword/Reset';
import ChangePassword from './pages/user/ForgotPassword/Change';

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
      // fontFamily: [
      //   'Handlee'
      // ].join(',')
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

        <Route path="/profile" element={<Profile />} />
        <Route path="/confirmEmail" element={<ConfirmEmail />} />
        <Route path="/resetPassword" element={<ResetPassword />} />
        <Route path="/resetPassword/:token" element={<ChangePassword />} />

        <Route path="/category" element={<CategoryTable />} />
        <Route path="/category/create" element={<CategoryCreate />} />
        <Route path="/category/update" element={<CategoryUpdate />} />

        <Route path="/CharacteristicGroup" element={<CharacteristicGroupTable />} />
        <Route path="/CharacteristicGroup/create" element={<CharacteristicGroupCreate />} />
        <Route path="/CharacteristicGroup/update" element={<CharacteristicGroupUpdate />} />

        <Route path="/characteristic" element={<CharacteristicTable />} />
        <Route path="/characteristic/create" element={<CharacteristicCreate />} />
        <Route path="/characteristic/update" element={<CharacteristicUpdate />} />


        {/* <Route path="*" element={<NotFound />} /> */}
      </Routes>
    </ThemeProvider>
  );
}

export default App;
