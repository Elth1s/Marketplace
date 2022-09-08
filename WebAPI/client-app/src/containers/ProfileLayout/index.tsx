import Box from "@mui/material/Box";
import Container from "@mui/material/Container";
import Grid from "@mui/material/Grid";

import { Outlet } from "react-router-dom";

import Sitebar from "./Sitebar"
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
            <Container component="main" sx={{ maxWidth: { xl: "xl", lg: "lg", md: "md" }, mb: 8, mt: "20px" }}>
                <Grid container>
                    <Grid item xs={3}>
                        <Sitebar />
                    </Grid>
                    <Grid item xs={9}>
                        <Outlet />
                    </Grid>
                </Grid>
            </Container>
            <Footer />
        </Box >
    );
}

export default ProfileLayout;