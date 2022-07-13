import { Box, styled } from "@mui/material";


export const BoxStyle = styled(Box)(({ theme }) => ({
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
    justifyContent: "space-between",
}));

export const ImageBoxStyle = styled(Box)(({ theme }) => ({
    margin: "auto",
}));