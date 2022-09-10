import Box from "@mui/material/Box";
import Container from "@mui/material/Container";
import Grid from "@mui/material/Grid";

import { Outlet } from "react-router-dom";

import Sidebar from "./Sidebar"
import Header from "../DefaultLayout/Header";
import Footer from "../DefaultLayout/Footer";

const ProfileLayout = () => {
    return (
        <Box
            sx={{
                display: 'flex',
                flexDirection: 'column',
                minHeight: '100vh',
            }}
        >
            <Header />
            <Container component="main" sx={{ maxWidth: { xl: "xl", lg: "lg", md: "md" }, mb: 8, mt: "40px" }}>
                <Grid container>
                    <Grid item xs={2}>
                        <Sidebar />
                    </Grid>
                    <Grid item xs={1} />
                    <Grid item xs={9}>
                        <Box sx={{ width: "1100px", ml: "auto" }}>
                            <Outlet />
                        </Box>
                    </Grid>
                </Grid>
            </Container>
            <Footer />
        </Box >
    );
}

export default ProfileLayout;