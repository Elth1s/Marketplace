import React, { useState } from "react";
import { AdminHeader, LeftMenu } from "./components";

const Admin = () => {

  const [isOpenMenu,setIsOpenMenu]=useState(true);

  const open = ()=>{        
      setIsOpenMenu((prev)=>!prev);
  }
return (
  <>
    <div style={{ display: "flex", width: "100%" }}>
      {isOpenMenu ? <LeftMenu />:null}
      <AdminHeader openMenu={open}  />
    </div>
  </>
);
};

export default Admin;
