import Container from "@mui/material/Container";

import { Outlet } from "react-router-dom";

import Header from "../DefaultLayout/Header";

const SellerLayout = () => {
    return (
        <>
            <Header />
            <Container component="main" sx={{ maxWidth: { xl: "xl", lg: "lg", md: "md" } }}>
                <Outlet />
            </Container>
        </>
    );
}

export default SellerLayout;