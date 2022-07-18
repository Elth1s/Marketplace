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
    color: "#FFF", 
    lineHeight: "1.25",
    fontWeight: "700",
}));

export const BodyStyle = styled(Typography)(() => ({
    color: "#FFF",
    lineHeight: "1.25",
    fontWeight: "500",
}));

export const LinkStyle = styled(Link)(() => ({
    color: "#FFF",
    lineHeight: "1.25",
    fontWeight: "500",
    textDecoration: "none",
}));

export const ItemBoxStyled = styled(Box)(() => ({
    marginBottom: "35px",
    "&:last-child": {
        marginBottom: "0px"
    }
}));