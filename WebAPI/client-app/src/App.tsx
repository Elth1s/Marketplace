import { Routes, Route } from 'react-router-dom';

import AuthLayout from './containers/AuthLayout';

import './App.css';
import SignIn from './pages/auth/SignIn';
import SignUp from './pages/auth/SignUp';
import Profile from './pages/user/Profile';

import CategoryTable from './pages/admin/category/Table';
import CategoryCreate from './pages/admin/category/Create';
import CategoryUpdate from './pages/admin/category/Update';

import CharacteristicGroupTable from './pages/admin/characteristicGroup/Table';
import CharacteristicGroupCreate from './pages/admin/characteristicGroup/Create';
import CharacteristicGroupUpdate from './pages/admin/characteristicGroup/Update';

import CharacteristicTable from './pages/admin/characteristic/Table';
import CharacteristicCreate from './pages/admin/characteristic/Create';
import CharacteristicUpdate from './pages/admin/characteristic/Update';

function App() {
  return (
    <>
      <Routes>

        <Route element={<AuthLayout />}>
          <Route path="/auth/signin" element={<SignIn />} />
          <Route path="/auth/signup" element={<SignUp />} />
          <Route path="/auth/profile" element={<Profile />} />

          <Route path="/category" element={<CategoryTable />} />
          <Route path="/category/create" element={<CategoryCreate />} />
          <Route path="/category/update" element={<CategoryUpdate />} />

          <Route path="/CharacteristicGroup" element={<CharacteristicGroupTable />} />
          <Route path="/CharacteristicGroup/create" element={<CharacteristicGroupCreate />} />
          <Route path="/CharacteristicGroup/update" element={<CharacteristicGroupUpdate />} />

          <Route path="/characteristic" element={<CharacteristicTable />} />
          <Route path="/characteristic/create" element={<CharacteristicCreate />} />
          <Route path="/characteristic/update" element={<CharacteristicUpdate />} />
        </Route>


        {/* <Route path="*" element={<NotFound />} /> */}
      </Routes>
    </>
  );
}

export default App;
