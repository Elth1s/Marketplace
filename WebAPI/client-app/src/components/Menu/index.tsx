import { AdminPanelSettingsOutlined, Login, Logout, NightlightOutlined, PersonOutlineOutlined } from '@mui/icons-material';
import {
    Menu,
    Typography,
    Box,
    IconButton,
    MenuItem,
    Divider,
    Switch
} from '@mui/material';
import React, { useState } from 'react'
import { useNavigate } from "react-router-dom";

import { useActions } from '../../hooks/useActions';
import { useTypedSelector } from '../../hooks/useTypedSelector';

import LinkRouter from '../LinkRouter';
import { MenuItemStyle } from './styled';

interface ISettingsMenuItem {
    label: string,
    icon: any,
    onClick: any,
    switchElement: boolean
}

const MainMenu = () => {

    const { /*SetTheme,*/ LogoutUser } = useActions();
    // const { darkTheme } = useTypedSelector((state) => state.ui);
    const [darkTheme, setDarkTheme] = useState<boolean>(false);
    const { user, isAuth } = useTypedSelector((state) => state.auth)
    const [anchorEl, setAnchorEl] = React.useState(null);
    const open = Boolean(anchorEl);

    const navigate = useNavigate();

    const UISettings: Array<ISettingsMenuItem> = [
        {
            label: 'Dark theme',
            icon: <NightlightOutlined />,
            onClick: () => handleThemeChange(),
            switchElement: true
        }
    ];


    const handleClick = (event: any) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    const handleThemeChange = () => {
        // SetTheme(!darkTheme)
        setDarkTheme(!darkTheme)
        // localStorage.darkTheme = !darkTheme;
    };

    const handleLogOut = () => {
        setAnchorEl(null);
        LogoutUser();
        navigate("/");
    }

    return (
        <>
            <IconButton
                sx={{ borderRadius: '12px', p: 0 }}
                size="large"
                aria-label="search"
                color="primary"
                onClick={handleClick}
            >
                <PersonOutlineOutlined sx={{ fontSize: "45px" }} />
            </IconButton>
            <Menu
                anchorEl={anchorEl}
                id="account-menu"
                open={open}
                onClose={handleClose}
                PaperProps={{
                    elevation: 0,
                    sx: {
                        borderRadius: 3,
                        overflow: 'visible',
                        filter: 'drop-shadow(0 4px 8px rgba(0,0,0,0.5))',
                        mt: 0.5,
                        minWidth: "200px",
                        '& .MuiAvatar-root': {
                            width: 32,
                            height: 32,
                            ml: -0.5,
                            mr: 1,
                        }
                    },
                }}
                transformOrigin={{ horizontal: 'right', vertical: 'top' }}
                anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
            >
                {isAuth &&
                    <Box>
                        <LinkRouter underline="none" color="unset" to="/profile" onClick={handleClose}>
                            <Box sx={{ my: 0.5, mb: 1.5, px: 2.5 }}>
                                <Typography variant="subtitle1" noWrap >
                                    {user.username}
                                </Typography>
                                <Typography variant="body2" noWrap>
                                    {user.email}
                                </Typography>
                            </Box>
                        </LinkRouter>
                        <Divider sx={{ my: 1, background: "#45a29e" }} />
                        {user.roles == "Admin" &&
                            <LinkRouter underline="none" color="unset" to="/admin" >
                                <MenuItemStyle                                >
                                    <IconButton sx={{ mr: 2, width: 24, height: 24, color: "secondary" }}>
                                        <AdminPanelSettingsOutlined />
                                    </IconButton>
                                    <Typography variant="subtitle1" noWrap sx={{ color: 'secondary' }}>
                                        Admin Panel
                                    </Typography>
                                </MenuItemStyle>
                            </LinkRouter>}
                    </Box>
                }
                {/* <Divider sx={{ my: 1, background: "#45a29e" }} /> */}
                {UISettings.map((option) => (
                    <MenuItemStyle
                        key={option.label}
                        onClick={option.onClick}
                    >
                        <IconButton sx={{ mr: 2, width: 24, height: 24 }}>
                            {option.icon}
                        </IconButton>
                        <Typography variant="subtitle1" noWrap sx={{ color: 'secondary' }}>
                            {option.label}
                        </Typography>
                        {option.switchElement &&
                            <Switch checked={darkTheme} />
                        }
                    </MenuItemStyle>
                ))}
                <Divider sx={{ my: 1, background: "#45a29e" }} />
                {isAuth
                    ?
                    <MenuItemStyle onClick={handleLogOut}>
                        <IconButton sx={{ mr: 2, width: 24, height: 24 }}>
                            <Logout />
                        </IconButton>
                        <Typography variant="subtitle1" noWrap >
                            Log Out
                        </Typography>
                    </MenuItemStyle>
                    :
                    <LinkRouter underline="none" color="unset" to="/auth/signin" >
                        <MenuItemStyle>
                            <IconButton sx={{ mr: 2, width: 24, height: 24, color: "secondary" }}>
                                <Login />
                            </IconButton>
                            <Typography variant="subtitle1" noWrap sx={{ color: 'secondary' }}>
                                Sign In
                            </Typography>
                        </MenuItemStyle>
                    </LinkRouter>}
            </Menu>
        </>
    )
}

export default MainMenu;