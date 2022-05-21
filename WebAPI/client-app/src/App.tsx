import { Routes, Route } from 'react-router-dom';

import AuthLayout from './components/containers/AuthLayout';

import './App.css';
import SignIn from './components/auth/SignIn';
import SignUp from './components/auth/SignUp';
import Profile from './components/user/Profile';

import CategoryTable from './components/category/Table';
import CategoryCreate from './components/category/Create';
import CategoryUpdate from './components/category/Update';

import CharacteristicGroupTable from './views/characteristicGroup/Table';
import CharacteristicGroupCreate from './views/characteristicGroup/Create';
import CharacteristicGroupUpdate from './views/characteristicGroup/Update';

import CharacteristicTable from './views/characteristic/Table';
import CharacteristicCreate from './views/characteristic/Create';
import CharacteristicUpdate from './views/characteristic/Update';

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
