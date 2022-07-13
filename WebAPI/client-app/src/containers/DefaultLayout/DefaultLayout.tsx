import { Container, IconButton } from "@mui/material";
import { ArrowUpward } from '@mui/icons-material';

import { useState, useEffect } from "react";
import { Outlet } from "react-router-dom";

import Header from "./Header";
import Footer from "./Footer";

const DefaultLayout = () => {
    const [showButton, setShowButton] = useState(false);
    useEffect(() => {
        window.addEventListener("scroll", () => {
            if (window.pageYOffset > 300) {
                setShowButton(true);
            } else {
                setShowButton(false);
            }
        });
    }, []);
    const scrollToTop = () => {
        window.scrollTo({
            top: 0,
            behavior: 'smooth'
        });
    };
    return (
        <>
            <Header />
            <Container component="main" sx={{ maxWidth: { xl: "xl", lg: "lg", md: "md" } }}>
                <Outlet />
                {showButton && (
                    <IconButton aria-label="edit" color="secondary" sx={{ border: 2, borderRadius: "12px" }} onClick={scrollToTop} style={{ position: "fixed", bottom: "20px", right: "20px" }}>
                        <ArrowUpward fontSize="large" />
                    </IconButton>
                )}
            </Container>
            <Footer />
        </>
    );
}

export default DefaultLayout;