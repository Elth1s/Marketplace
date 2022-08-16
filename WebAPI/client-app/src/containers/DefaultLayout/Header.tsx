import {
    AppBar,
    IconButton,
    Toolbar,
    InputAdornment,
    Box,
    Container,
    Divider
} from "@mui/material";
import { useState } from "react";
import { useTranslation } from "react-i18next";

import { useTypedSelector } from "../../hooks/useTypedSelector";

import { logo } from "../../assets/logos"

import { LanguageButtonStyle, TextFieldStyle } from "./styled";

import MainMenu from "../../components/Menu";
import LinkRouter from "../../components/LinkRouter";
import Basket from "../../components/Basket";

import { orange_heart, search } from '../../assets/icons';
import CatalogMenu from "../../components/CatalogMenu";

const Header = () => {
    const { t, i18n } = useTranslation()
    const { isAuth } = useTypedSelector((state) => state.auth)


    const [isUaLanguage, setIsUaLanguage] = useState<boolean>(i18n.language == "en" ? false : true);

    const changeLanguage = (isUa: boolean) => {
        if (isUa == isUaLanguage)
            return;
        setIsUaLanguage(isUa);
        i18n.changeLanguage(isUa ? "ua" : "en")
        window.location.reload();
    }

    return (
        <AppBar component="header" sx={{ height: "110px", mb: "30px", mt: "15px" }} elevation={0} position="static" >
            <Container sx={{ height: "100%", maxWidth: { xl: "xl", lg: "lg", md: "md" } }}>
                <Toolbar disableGutters={true} sx={{ height: "100%", display: "flex", justifyContent: "space-between", alignItems: "center" }}>
                    <LinkRouter underline="none" color="unset" to="/">
                        <img
                            style={{ height: "82px" }}
                            src={logo}
                            alt="logo"
                        />
                    </LinkRouter>
                    <CatalogMenu />
                    <TextFieldStyle placeholder={t('containers.default.header.searchProducts')}
                        InputProps={{
                            endAdornment: (
                                <InputAdornment position="end" >
                                    <IconButton sx={{ padding: "3px", borderRadius: '12px' }} edge="start">
                                        <img
                                            style={{ width: "35px", height: "35px" }}
                                            src={search}
                                            alt="icon"
                                        />
                                    </IconButton>
                                </InputAdornment>
                            )
                        }} />
                    <Box sx={{ display: "flex" }}>
                        <LanguageButtonStyle selected={!isUaLanguage} onClick={() => { changeLanguage(false) }}>EN</LanguageButtonStyle>
                        <Divider sx={{ borderColor: "black", borderRightWidth: "3px", mx: "1px" }} orientation="vertical" flexItem />
                        <LanguageButtonStyle selected={isUaLanguage} onClick={() => { changeLanguage(true) }}>UA</LanguageButtonStyle>
                    </Box>
                    {isAuth &&
                        <>
                            <IconButton
                                sx={{ borderRadius: '12px', p: 0.5 }}
                                size="large"
                                aria-label="search"
                                color="primary"
                            >
                                <img
                                    style={{ width: "40px", height: "40px" }}
                                    src={orange_heart}
                                    alt="icon"
                                />
                            </IconButton>
                        </>}
                    <Basket />
                    <MainMenu />
                </Toolbar>
            </Container>
        </AppBar >
    );
};

export default Header;