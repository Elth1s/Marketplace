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

    border: `1px solid ${theme.palette.mode == "dark" ? "#AEAEAE" : "#7e7e7e"}`,
    borderRadius: "10px",
    paddingLeft: "21px",
    paddingRight: "21px",
    paddingTop: "23px",
    paddingBottom: "28px",
    display: "flex",
    flexDirection: "column",
}));

export const SmallBoxStyle = styled(Box)(({ theme }) => ({
    background: theme.palette.mode == "dark" ? "#2D2D2D" : "transparent",
    height: "300px",
    [theme.breakpoints.only('xl')]: {
        width: "216px",
    },
    [theme.breakpoints.down('xl')]: {
        width: "175pxpx",
    },

    border: `1px solid ${theme.palette.mode == "dark" ? "#2D2D2D" : "#7e7e7e"}`,
    borderRadius: "10px",
    paddingLeft: "15px",
    paddingRight: "15px",
    paddingTop: "16px",
    paddingBottom: "16px",
    display: "flex",
    flexDirection: "column",
}));

export const ImageBoxStyle = styled(Box)(({ theme }) => ({
    margin: "auto",
    marginTop: "0px",
    marginBottom: "15px"
}));

export const SmallImageBoxStyle = styled(Box)(({ theme }) => ({
    margin: "auto",
    marginTop: "0px",
    marginBottom: "10px"
}));