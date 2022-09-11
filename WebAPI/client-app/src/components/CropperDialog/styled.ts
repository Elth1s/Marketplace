import { Box, styled } from "@mui/material";



export const BoxStyle = styled(Box, {
    shouldForwardProp: (prop) => prop !== 'imgSrc' && prop !== "isDark" && prop !== "isGreen"
})<{ imgSrc: string; isDark: boolean; isGreen: boolean; }>(({ theme, imgSrc, isDark, isGreen }) => ({
    width: "100px",
    height: "100px",
    background: isDark ? "black" : "white",
    border: imgSrc === "" ? `1px solid ${isGreen ? "#0E7C3A" : "#F45626"}` : "0px solid #F45626",
    boxShadow: imgSrc === "" ? "0" : "0px 3px 1px -2px rgb(0 0 0 / 20%), 0px 2px 2px 0px rgb(0 0 0 / 14%), 0px 1px 5px 0px rgb(0 0 0 / 12%)",
    borderRadius: "10px",
    cursor: "pointer"
}),
);