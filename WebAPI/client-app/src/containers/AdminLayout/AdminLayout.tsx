import { Grid } from "@mui/material";
import React, { useState } from "react";
import { Outlet } from "react-router-dom";
import AdminHeader from "../../pages/admin/panel/admin_header";
import LeftMenu from "../../pages/admin/panel/admin_left_menu";

const AdminLayout = () => {

  const [isOpenMenu,setIsOpenMenu]=useState(true);

  const open = ()=>{        
      setIsOpenMenu((prev)=>!prev);
  }
   return (
    <Grid container>
    {isOpenMenu ? <Grid lg={3}>
      <LeftMenu/>
    </Grid>:null}     
    <Grid lg={isOpenMenu ? 9 : 12}>
      <AdminHeader openMenu={open}/>
      <Outlet/>
    </Grid>
  </Grid>
);
};

export default AdminLayout;