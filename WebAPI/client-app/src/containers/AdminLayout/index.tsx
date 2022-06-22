import Box from '@mui/material/Box';

import { useState } from "react";
import { Outlet } from "react-router-dom";

import Header from './Header';
import Sitebar from './Sidebar';

const AdminLayout = () => {
    const [open, setOpen] = useState(true);

    const handleDrawerToggle = () => {
        setOpen(!open);
    };

    return (
        <Box sx={{ display: 'flex', width: '100%', height: '100vh' }}>
            <Sitebar open={open} />
            <Box component="main" sx={{ width: '100%', flexGrow: 1 }}>
                <Header handleDrawerToggle={handleDrawerToggle} />
                <Box sx={{ p: { xs: 2, sm: 3 } }}>
                    <Outlet />
                </Box>
            </Box>
        </Box>
    );
};

export default AdminLayout;