import Box from "@mui/material/Box";
import TextField from "@mui/material/TextField";
import Typography from "@mui/material/Typography";
import Link from "@mui/material/Link";

import { styled } from "@mui/material";

export const BoxContainer = styled(Box)(({ theme }) => ({
    width: "1560px",
    height: "100%",
    marginLeft: "auto",
    marginRight: "auto",
}));

export const TextFieldStyle = styled(TextField)(({ theme }) => ({
    width: "660px",
    height: "58px",
    "& fieldset": {
        borderRadius: "9px",
        borderColor: "#7E7E7E",
    },
    InputLabelProps: {
        color: "#7E7E7E"
    },
    inputProps: {
        height: "58px",
        paddingTop: "0px",
        paddingBottom: "0px",
        fontSize: "18px"
    },
    "& .MuiOutlinedInput-root": {
        color: "#777777",
        "& fieldset": {
            borderColor: "#7E7E7E"
        }
    }
}));

export const TitleStyle = styled(Typography)(() => ({
    color: "#FFFFFF",
    fontSize: "20px",
    fontWeight: "700",
    marginBottom: "65px"
}));

export const DescriptionStyle = styled(Typography)(() => ({
    display: "flex",
    color: "#FFFFFF",
    fontSize: "20px",
    fontWeight: "500",
    marginBottom: "35px",
    "&:last-child":{
        marginBottom: "0px"
    }
}));

export const DescriptionLinkStyle = styled(Link)(() => ({
    display: "flex",
    color: "#FFFFFF",
    fontSize: "20px",
    fontWeight: "500",
    textDecoration: "none",
    textDecorationColor: "trancparent",
    marginBottom: "35px",
    "&:last-child":{
        marginBottom: "0px"
    }
}));

export const BoxStyle = styled(Box)(({ theme }) => ({
    backgroundColor: theme.palette.secondary.main
}));