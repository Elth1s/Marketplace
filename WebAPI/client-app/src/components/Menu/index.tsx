import { AdminPanelSettingsOutlined, Login, Logout, NightlightOutlined, PersonOutlineOutlined, Store } from '@mui/icons-material';
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

import AuthDialog from '../../pages/auth';

import LinkRouter from '../LinkRouter';

import { MenuItemStyle } from './styled';

import { orange_user } from '../../assets/icons';
import CreateShopDialog from '../../pages/seller/CreateShopDialog';
import { useTranslation } from 'react-i18next';

interface ISettingsMenuItem {
    label: string,
    icon: any,
    onClick: any,
    switchElement: boolean
}

const MainMenu = () => {
    const { t } = useTranslation()
    const { SetTheme, LogoutUser, ResetBasket } = useActions();
    const { isDarkTheme } = useTypedSelector((state) => state.ui);

    const { AuthDialogChange } = useActions();
    const { user, isAuth } = useTypedSelector((state) => state.auth)

    const [anchorEl, setAnchorEl] = React.useState(null);

    const open = Boolean(anchorEl);
    const [shopDialogOpen, setShopDialogOpen] = useState<boolean>(false);

    const navigate = useNavigate();

    const UISettings: Array<ISettingsMenuItem> = [
        {
            label: `${t('containers.default.header.userMenu.darkTheme')}`,
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

    const shopDialogClose = () => {
        setShopDialogOpen(false);
    };

    const handleThemeChange = () => {
        SetTheme(!isDarkTheme)
        localStorage.darkTheme = !isDarkTheme;
    };

    const handleLogOut = () => {
        setAnchorEl(null);
        LogoutUser();
        ResetBasket();
        navigate("/");
    }

    return (
        <>
            <IconButton
                sx={{ borderRadius: '12px', p: 0.5 }}
                size="large"
                aria-label="search"
                color="primary"
                onClick={handleClick}
            >
                <img
                    style={{ width: "40px", height: "40px" }}
                    src={orange_user}
                    alt="icon"
                />
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
                        <LinkRouter underline="none" color="unset" to="/profile/information" onClick={handleClose}>
                            <Box sx={{ my: 0.5, mb: 1.5, px: 2.5 }}>
                                <Typography variant="h6" noWrap color="inherit">
                                    {user.firstName} {user.secondName}
                                </Typography>
                                <Typography variant="body2" noWrap color="inherit">
                                    {user.emailOrPhone}
                                </Typography>
                            </Box>
                        </LinkRouter>
                        <Divider sx={{ my: 1, background: "#45a29e" }} />
                        {user.role == "Admin" &&
                            <LinkRouter underline="none" color="unset" to="/admin/categories" >
                                <MenuItemStyle >
                                    <IconButton sx={{ mr: 2, width: 24, height: 24, color: "secondary" }}>
                                        <AdminPanelSettingsOutlined />
                                    </IconButton>
                                    <Typography variant="h6" noWrap color="inherit">
                                        {t('containers.default.header.userMenu.adminPanel')}
                                    </Typography>
                                </MenuItemStyle>
                            </LinkRouter>}
                    </Box>
                }
                {/* <Divider sx={{ my: 1, background: "#45a29e" }} /> */}
                {isAuth && <Box>
                    {
                        user.role == "Admin" || user.role == "Seller"
                            ? <LinkRouter underline="none" color="unset" to="/seller/products" >
                                <MenuItemStyle                                >
                                    <IconButton sx={{ mr: 2, width: 24, height: 24, color: "secondary" }}>
                                        <Store />
                                    </IconButton>
                                    <Typography variant="h6" noWrap color="inherit">
                                        {t('containers.default.header.userMenu.sellerPanel')}
                                    </Typography>
                                </MenuItemStyle>
                            </LinkRouter>
                            : <MenuItemStyle onClick={() => { handleClose(); setShopDialogOpen(true); }}>
                                <IconButton sx={{ mr: 2, width: 24, height: 24, color: "secondary" }}>
                                    <Store />
                                </IconButton>
                                <Typography variant="h6" noWrap color="inherit">
                                    {t('containers.default.header.userMenu.createShop')}
                                </Typography>
                            </MenuItemStyle>
                    }
                </Box>}
                {UISettings.map((option) => (
                    <MenuItemStyle
                        key={option.label}
                        onClick={option.onClick}
                    >
                        <IconButton sx={{ mr: 2, width: 24, height: 24 }}>
                            {option.icon}
                        </IconButton>
                        <Typography variant="h6" noWrap color="inherit">
                            {option.label}
                        </Typography>
                        {option.switchElement &&
                            <Switch checked={isDarkTheme} />
                        }
                    </MenuItemStyle>
                ))}
                <Divider sx={{ my: 1, background: "#45a29e" }} />
                {isAuth
                    ? <MenuItemStyle onClick={handleLogOut}>
                        <IconButton sx={{ mr: 2, width: 24, height: 24 }}>
                            <Logout />
                        </IconButton>
                        <Typography variant="h6" noWrap color="inherit">
                            {t('containers.default.header.userMenu.logOut')}
                        </Typography>
                    </MenuItemStyle>
                    : <MenuItemStyle onClick={() => { handleClose(); AuthDialogChange(); }}>
                        <IconButton sx={{ mr: 2, width: 24, height: 24, color: "secondary" }}>
                            <Login />
                        </IconButton>
                        <Typography variant="h6" noWrap color="inherit">
                            {t('containers.default.header.userMenu.signIn')}
                        </Typography>
                    </MenuItemStyle>
                }
            </Menu>
            <CreateShopDialog dialogOpen={shopDialogOpen} dialogClose={shopDialogClose} />
            <AuthDialog />
        </>
    )
}

export default MainMenu;