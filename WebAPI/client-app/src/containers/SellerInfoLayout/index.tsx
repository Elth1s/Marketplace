import Box  from "@mui/material/Box";
import Container from "@mui/material/Container";

import { Outlet } from "react-router-dom";
import Header from "../DefaultLayout/Header";

const SellerInfoLayout = ()=> {
        return (
            <Box
                sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    minHeight: '100vh',
                    width: "100%"
                }}
            >
                <Header />
                <Container component="main" sx={{ maxWidth: { xl: "xl", lg: "lg", md: "md" }, mb: 8 }}>
                    <Outlet />
                </Container>
            </Box>
        );
}

export default SellerInfoLayout;