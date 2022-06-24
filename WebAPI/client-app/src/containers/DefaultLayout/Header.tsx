import {
    AppBar,
    IconButton,
    Toolbar,
    TextField,
    InputAdornment,
    Button,
    Box
} from "@mui/material";
import {
    Search,
    FavoriteBorderOutlined,
    ShoppingCartOutlined,
    FormatListBulletedOutlined
} from "@mui/icons-material";
import React, { useState } from "react";
import { Link } from "react-router-dom";
import { useActions } from "../../hooks/useActions";
import { useTypedSelector } from "../../hooks/useTypedSelector";

import { logo } from "../../assets/logos"
import MainMenu from "../../components/Menu";
import { BoxContainer, TextFieldStyle } from "./styled";

const Header = () => {
    const { isAuth } = useTypedSelector((state) => state.auth)

    return (
        <AppBar component="header" sx={{ height: "158px", paddingTop: "43px" }} elevation={0} position="static" >
            <BoxContainer >
                <Toolbar disableGutters={true} sx={{ height: "100%", display: "flex", justifyContent: "space-between", alignItems: "end" }}>
                    <Link to="/" style={{ height: "110px", textDecoration: 'none', color: 'unset' }}>
                        <img
                            style={{ height: "110px", cursor: "pointer" }}
                            src={logo}
                            alt="logo"
                        />
                    </Link>
                    <Button variant="contained" sx={{ width: "148px", height: "48px", fontSize: "18px", paddingLeft: "13px", paddingRight: "17px", textTransform: "none", borderRadius: "9px" }}
                        startIcon={<FormatListBulletedOutlined />}>
                        Catalog
                    </Button>
                    <TextFieldStyle placeholder="Search products..."
                        InputProps={{
                            endAdornment: (
                                <InputAdornment position="end" >
                                    <IconButton sx={{ padding: "3px", borderRadius: '12px' }} edge="start">
                                        <Search color="primary" sx={{ width: "40px", height: "40px" }} />
                                    </IconButton>
                                </InputAdornment>
                            )
                        }} />
                    <Box sx={{ flexGrow: 0.2 }}></Box>
                    <Box sx={{ height: "58px", flexGrow: 0.25, display: "flex", justifyContent: "space-between", alignItems: "center" }}>
                        {isAuth &&
                            <>
                                <IconButton
                                    sx={{ borderRadius: '12px', p: 0 }}
                                    size="large"
                                    aria-label="search"
                                    color="primary"
                                >
                                    <FavoriteBorderOutlined sx={{ fontSize: "45px" }} />
                                </IconButton>
                                <IconButton
                                    sx={{ borderRadius: '12px', p: 0 }}
                                    size="large"
                                    aria-label="search"
                                    color="primary"
                                >
                                    <ShoppingCartOutlined sx={{ fontSize: "45px" }} />
                                </IconButton>
                            </>}
                        <MainMenu />
                    </Box>
                </Toolbar>
            </BoxContainer>
        </AppBar>
    );
};

export default Header;