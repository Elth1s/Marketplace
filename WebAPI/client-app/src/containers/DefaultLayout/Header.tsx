import {
    AppBar,
    Avatar,
    Box,
    Button,
    Container,
    Divider,
    IconButton,
    Menu,
    MenuItem,
    Switch,
    Toolbar,
    Typography,
} from "@mui/material";
import {
    Login,
    Logout,
    NightlightOutlined,
    PersonOutlineOutlined,
    AdminPanelSettingsOutlined,
    ListOutlined,
    FavoriteBorderOutlined,
    ShoppingCartOutlined
} from "@mui/icons-material";
import React, { useState } from "react";
import { Link } from "react-router-dom";
import { useActions } from "../../hooks/useActions";
import { useTypedSelector } from "../../hooks/useTypedSelector";

import { logo } from "../../assets/logos"
import MainMenu from "../../components/Menu";

const Header = () => {
    const { LogoutUser } = useActions();
    const { isAuth } = useTypedSelector((state) => state.auth)

    return (
        <Box sx={{ flexGrow: 1 }} mb={{ xs: 9, sm: 11 }} >
            <AppBar sx={{ borderBottom: 1, borderColor: '#F45626' }} position="fixed" >
                <Container sx={{ maxWidth: { xl: "xl", lg: "lg", md: "md" } }}>
                    <Toolbar style={{ paddingLeft: 0, paddingRight: 0 }}>
                        <Link to="/" style={{ textDecoration: 'none', color: 'unset' }} /*onClick={handleClose}*/>
                            <img

                                style={{ cursor: "pointer", height: "60px", marginTop: "5px", marginBottom: "5px" }}
                                src={logo}
                                alt="logo"
                            />
                        </Link>
                        <Button variant="contained" size="medium" sx={{ marginX: 4 }} startIcon={<ListOutlined />}>
                            Catalog
                        </Button>
                        <Box sx={{ flexGrow: 1 }} />

                        {isAuth &&
                            <>
                                <Button
                                    sx={{
                                        minWidth: 36,
                                        height: 36,
                                        p: 0,
                                        borderRadius: 2
                                    }}
                                    size="medium"
                                    color="primary"
                                >
                                    <FavoriteBorderOutlined sx={{ fontSize: "30px" }} />
                                </Button>

                                <Button
                                    sx={{
                                        minWidth: 36,
                                        height: 36,
                                        p: 0,
                                        borderRadius: 2,
                                        marginX: 2
                                    }}
                                    size="medium"
                                    color="primary"
                                >
                                    <ShoppingCartOutlined sx={{ fontSize: "30px" }} />
                                </Button>
                            </>}
                        <MainMenu />
                    </Toolbar>
                </Container>
            </AppBar>
        </Box >
    );
};

export default Header;