import { Box, Container, IconButton } from "@mui/material";
import { ArrowUpward } from '@mui/icons-material';
import { useState, useEffect } from "react";
import { Outlet } from "react-router-dom";
import Header from "./Header";
import { BoxContainer } from "./styled";

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
            <Container component="main" sx={{ maxWidth: { xl: "xl", lg: "lg", md: "md" }}}>
                <Outlet />
                {showButton && (
                    <IconButton aria-label="edit" sx={{ border: 2, borderColor: "#45A29E", borderRadius: 3, color: "#f1f1f1" }} onClick={scrollToTop} style={{ position: "fixed", bottom: "20px", right: "20px" }}>
                        <ArrowUpward fontSize="large" />
                    </IconButton>
                )}
            </Container>
        </>
    );
}

export default DefaultLayout;