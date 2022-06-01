import { Routes, Route } from 'react-router-dom';

import AuthLayout from './components/containers/AuthLayout';

import './App.css';
import SignIn from './components/auth/SignIn';
import SignUp from './components/auth/SignUp';
import Profile from './components/user/Profile';
import Admin from './components/admin_panel';
import AdminLayout from './components/containers/AdminLayout/AdminLayout';
function App() {
  return (
    <>
      <Routes>

        {/* <Route element={<AuthLayout />}>
          <Route path="/auth/signin" element={<SignIn />} />
          <Route path="/auth/signup" element={<SignUp />} />
          <Route path="/auth/profile" element={<Profile />} />
        </Route> */}


        <Route path='/admin/' element={<AdminLayout/>}>
            <Route path="auth/signin" element={<SignIn />} />
        </Route>
        {/* <Route path="*" element={<NotFound />} /> */}
      </Routes>
    </>
  );
}

export default App;
