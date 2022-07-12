import Container from "@mui/material/Container";
import Grid from "@mui/material/Grid";

import { Outlet } from "react-router-dom";

import Sitebar from "./Sitebar"
import Header from "../DefaultLayout/Header";
import Footer from "../DefaultLayout/Footer";

const ProfileLayout = () => {
    return (
        <>
            <Header />
            <Container component="main" sx={{ maxWidth: { xl: "xl", lg: "lg", md: "md" } }}>
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
        </>
    );
}

export default ProfileLayout;