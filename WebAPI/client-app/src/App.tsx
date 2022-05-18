import { Routes, Route } from 'react-router-dom';

import AuthLayout from './components/containers/AuthLayout';

import './App.css';
import SignIn from './components/auth/SignIn';
import SignUp from './components/auth/SignUp';
import Profile from './components/user/Profile';
import Admin from './components/admin_panel';
function App() {
  return (
    <>
      <Routes>

        <Route element={<AuthLayout />}>
          <Route path="/auth/signin" element={<SignIn />} />
          <Route path="/auth/signup" element={<SignUp />} />
          <Route path="/auth/profile" element={<Profile />} />
          <Route path='/admin' element={<Admin/>}/>
        </Route>


        {/* <Route path="*" element={<NotFound />} /> */}
      </Routes>
    </>
  );
}

export default App;
