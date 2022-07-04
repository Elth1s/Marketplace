import Box from '@mui/material/Box';
import Divider from "@mui/material/Divider";
import Typography from '@mui/material/Divider';

import { styled } from '@mui/material';

export const PriceBox = styled(Box)(() => ({
    display: "flex",
    flexDirection: "column",
    justifyContent: "center",
    alignItems: "flex-start"
}))

export const RatingBox = styled(Box)(({ theme }) => ({
    display: "flex",
    mb: "45px"
}))

export const CharacteristicBox = styled(Box)(({ theme }) => ({
    position: "relative",
    display: "flex",
    justifyContent: "space-between",
    mb: "20px",
}))

export const CharacteristicTypography = styled(Typography)(() => ({
    background: "#FFF",
    fontSize: "20px"
}))

export const CharacteristicDivider = styled(Divider)(() => ({
    position: "absolute",
    height: "1px",
    width: "100%",
    bottom: "9px",
    borderColor: "#000000",
    borderStyle: "dotted",
    borderBottomWidth: "revert",
    zIndex: "-1",
}))