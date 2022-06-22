import Box from '@mui/material/Box';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';

import SearchIcon from '@mui/icons-material/Search';

import { FC } from 'react';
import { IHeader } from './type';

import Profile from './Profile';
import Notification from './Notification'

const Header: FC<IHeader> = ({ handleDrawerToggle }) => {
    return (
        <Box sx={{ flexGrow: 1 }}>
            <AppBar position="static" color="transparent">
                <Toolbar>
                    <Box sx={{ flexGrow: 1 }}>
                        <IconButton
                            size="large"
                            edge="start"
                            color="inherit"
                            aria-label="menu"
                            onClick={handleDrawerToggle}
                            sx={{ mr: 2 }}
                        >
                            <MenuIcon />
                        </IconButton>
                    </Box>

                    <Box sx={{ flexShrink: 0, ml: 0.75 }}>
                        <IconButton size="large" aria-label="search" color="inherit">
                            <SearchIcon />
                        </IconButton>
                    </Box>
                    <Notification />
                    <Profile />

                </Toolbar>
            </AppBar>
        </Box >
    )
};

export default Header;