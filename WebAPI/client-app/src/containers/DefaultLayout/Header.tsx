import {
    AppBar,
    IconButton,
    Toolbar,
    InputAdornment,
    Button,
    Box,
    Container
} from "@mui/material";
import {
    Search,
    FavoriteBorderOutlined,
    ShoppingCartOutlined,
    FormatListBulletedOutlined
} from "@mui/icons-material";

import { useActions } from "../../hooks/useActions";
import { useTypedSelector } from "../../hooks/useTypedSelector";

import { logo } from "../../assets/logos"

import { TextFieldStyle } from "./styled";

import MainMenu from "../../components/Menu";
import LinkRouter from "../../components/LinkRouter";
import Basket from "../../components/Basket";

const Header = () => {
    const { isAuth } = useTypedSelector((state) => state.auth)

    return (
        <AppBar component="header" sx={{ height: "88px", marginBottom: "30px" }} elevation={0} position="static" >
            <Container sx={{ height: "100%", maxWidth: { xl: "xl", lg: "lg", md: "md" } }}>
                <Toolbar disableGutters={true} sx={{ height: "100%", display: "flex", justifyContent: "space-between", alignItems: "center" }}>
                    <LinkRouter underline="none" color="unset" to="/">
                        <img
                            style={{ height: "66px" }}
                            src={logo}
                            alt="logo"
                        />
                    </LinkRouter>
                    <LinkRouter underline="none" color="unset" to="/catalog">
                        <Button variant="contained" sx={{ width: "148px", height: "50px", fontSize: "18px", paddingLeft: "13px", paddingRight: "17px", textTransform: "none", borderRadius: "9px" }}
                            startIcon={<FormatListBulletedOutlined />}>
                            Catalog
                        </Button>
                    </LinkRouter>
                    <TextFieldStyle placeholder="Search products..."
                        InputProps={{
                            endAdornment: (
                                <InputAdornment position="end" >
                                    <IconButton sx={{ padding: "3px", borderRadius: '12px' }} edge="start">
                                        <Search color="primary" sx={{ fontSize: "40px" }} />
                                    </IconButton>
                                </InputAdornment>
                            )
                        }} />
                    <Box sx={{ flexGrow: 0.55 }} />
                    {isAuth &&
                        <>
                            <IconButton
                                sx={{ borderRadius: '12px', p: 0.5 }}
                                size="large"
                                aria-label="search"
                                color="primary"
                            >
                                <FavoriteBorderOutlined sx={{ fontSize: "35px" }} />
                            </IconButton>
                            <Basket />
                        </>}
                    <MainMenu />
                </Toolbar>
            </Container>
        </AppBar>
    );
};

export default Header;