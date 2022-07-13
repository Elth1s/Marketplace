import { styled, TextField, Typography, Link, Box } from "@mui/material";


export const TextFieldStyle = styled(TextField)(({ theme }) => ({
    width: "660px",
    "& fieldset": {
        borderRadius: "9px",
        borderColor: "#7E7E7E",
    },
    InputLabelProps: {
        color: "#7E7E7E"
    },
    inputProps: {
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
    "&:last-child": {
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
    "&:last-child": {
        marginBottom: "0px"
    }
}));

export const BoxStyle = styled(Box)(({ theme }) => ({
    backgroundColor: theme.palette.secondary.main,
    marginTop: "55px",
    paddingTop: "40px",
    paddingBottom: "40px"
}));