import { Box, styled } from "@mui/material";




export const BoxIconStyle = styled(Box, {
    shouldForwardProp: (prop) => prop !== 'iconBackground'
})<{ iconBackground: string; }>(({ theme, iconBackground }) => ({
    width: "50px",
    height: "100%",
    background: iconBackground
}),
);