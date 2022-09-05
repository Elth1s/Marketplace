import { Box, styled } from "@mui/material";



export const BoxStyle = styled(Box, {
    shouldForwardProp: (prop) => prop !== 'imgSrc' && prop !== "isDark"
})<{ imgSrc: string; isDark: boolean; }>(({ theme, imgSrc, isDark }) => ({
    width: "100px",
    height: "100px",
    background: isDark ? "black" : "white",
    border: imgSrc === "" ? "1px solid #F45626" : "0px solid #F45626",
    boxShadow: imgSrc === "" ? "0" : "0px 3px 1px -2px rgb(0 0 0 / 20%), 0px 2px 2px 0px rgb(0 0 0 / 14%), 0px 1px 5px 0px rgb(0 0 0 / 12%)",
    borderRadius: "10px",
    cursor: "pointer"
}),
);