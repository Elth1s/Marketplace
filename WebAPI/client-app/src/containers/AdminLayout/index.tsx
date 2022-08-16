import Box from '@mui/material/Box';

import { useState } from "react";
import { Outlet } from "react-router-dom";

import Header from './Header';
import Sidebar from './Sidebar';

const AdminLayout = () => {
    const [open, setOpen] = useState(true);

    const handleDrawerToggle = () => {
        setOpen(!open);
    };

    return (
        <Box sx={{ width: '100%', height: '100vh' }}>
            <Header handleDrawerToggle={handleDrawerToggle} />
            <Box component="main" sx={{ width: '100%', display: "flex" }}>
                <Sidebar open={open} />
                <Box sx={{ px: { xs: 3, sm: 5 }, py: { xs: 1, sm: 2 }, width: '100%' }}>
                    <Outlet />
                </Box>
            </Box>
        </Box>
    );
};

export default AdminLayout;