import { Box, styled } from "@mui/material";


export const BoxStyle = styled(Box)(({ theme }) => ({
    background: theme.palette.mode == "dark" ? "#2D2D2D" : "transparent",
    height: "415px",
    [theme.breakpoints.only('xl')]: {
        width: "300px",
    },
    [theme.breakpoints.down('xl')]: {
        width: "285px",
    },

    border: "1px solid #7e7e7e",
    borderRadius: "10px",
    padding: "35px",
    display: "flex",
    flexDirection: "column",
}));

export const ImageBoxStyle = styled(Box)(({ theme }) => ({
    margin: "auto",
    marginTop: "0px"
}));