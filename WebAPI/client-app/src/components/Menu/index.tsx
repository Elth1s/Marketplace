import { AdminPanelSettingsOutlined, Login, Logout, NightlightOutlined, PersonOutlineOutlined } from '@mui/icons-material';
import {
    Button,
    Menu,
    Typography,
    Box,
    IconButton,
    MenuItem,
    Divider,
    Switch
} from '@mui/material';
import React, { useState } from 'react'
import { Link, useNavigate } from "react-router-dom";
import { useActions } from '../../hooks/useActions';
import { useTypedSelector } from '../../hooks/useTypedSelector';

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
                        <Link to="/profile" style={{ textDecoration: 'none', color: 'unset' }} onClick={handleClose}>
                            <Box sx={{ my: 0.5, mb: 1.5, px: 2.5 }}>
                                <Typography variant="subtitle1" noWrap >
                                    {user.username}
                                </Typography>
                                <Typography variant="body2" noWrap>
                                    {user.email}
                                </Typography>
                            </Box>
                        </Link>
                        <Divider sx={{ my: 1, background: "#45a29e" }} />
                        {user.roles == "Admin" &&
                            <Link to="/admin" style={{ textDecoration: 'none', color: 'unset' }}>
                                <MenuItem
                                    sx={{ py: 1, px: 2.5 }}

                                >
                                    <IconButton sx={{ mr: 2, width: 24, height: 24, color: "secondary" }}>
                                        <AdminPanelSettingsOutlined />
                                    </IconButton>
                                    <Typography variant="subtitle1" noWrap sx={{ color: 'secondary' }}>
                                        Admin Panel
                                    </Typography>
                                </MenuItem>
                            </Link>}
                    </Box>
                }
                {/* <Divider sx={{ my: 1, background: "#45a29e" }} /> */}
                {UISettings.map((option) => (
                    <MenuItem
                        key={option.label}
                        onClick={option.onClick}
                        sx={{ py: 1, px: 2.5 }}
                        style={{ textDecoration: 'none', color: 'unset' }}
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
                    </MenuItem>
                ))}
                <Divider sx={{ my: 1, background: "#45a29e" }} />
                {isAuth
                    ?
                    <MenuItem
                        onClick={handleLogOut}
                        sx={{ py: 1, px: 2.5 }}
                        style={{ textDecoration: 'none', color: 'unset' }}
                    >
                        <IconButton sx={{ mr: 2, width: 24, height: 24 }}>
                            <Logout />
                        </IconButton>
                        <Typography variant="subtitle1" noWrap >
                            Log Out
                        </Typography>
                    </MenuItem>
                    :
                    <Link to="/auth/signin" style={{ textDecoration: 'none', color: 'unset' }}>
                        <MenuItem
                            sx={{ py: 1, px: 2.5 }}

                        >
                            <IconButton sx={{ mr: 2, width: 24, height: 24, color: "secondary" }}>
                                <Login />
                            </IconButton>
                            <Typography variant="subtitle1" noWrap sx={{ color: 'secondary' }}>
                                Sign In
                            </Typography>
                        </MenuItem>
                    </Link>}
            </Menu>
        </>
    )
}

export default MainMenu;