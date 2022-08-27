import { styled, TextField, Button, Drawer, Typography, Link, Box } from "@mui/material";


export const TextFieldStyle = styled(TextField)(({ theme }) => ({
    width: "678px",

    "& fieldset": {
        borderRadius: "10px",
        borderColor: "#7E7E7E",

    },
    InputLabelProps: {
        color: "#7E7E7E",

    },
    inputProps: {

    },
    "& .MuiOutlinedInput-root": {
        color: "#777777",

        fontSize: "20px",
        lineHeight: "25px",
        "& fieldset": {
            borderColor: "#7E7E7E"
        },
        "&:hover fieldset": {
            borderColor: "#7E7E7E"
        },
        "&.Mui-focused fieldset": {
            borderColor: "#7E7E7E"
        },
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
    marginBottom: "30px",
    display: "flex",
    alignItems: "center",
    "&:last-child": {
        marginBottom: "0px"
    }
}));

export const LanguageButtonStyle = styled(Button, {
    shouldForwardProp: (prop) => prop !== 'selected'
})<{ selected?: boolean; }>(({ theme, selected }) => ({
    color: selected ? theme.palette.primary.main : "inherit",
    fontSize: "20px",
    lineHeight: "25px",
    fontWeight: "bold",
    minWidth: "auto",
    height: "auto",
    padding: 0,
    "&:hover": { background: "transparent" },
    "&& .MuiTouchRipple-child": { backgroundColor: "transparent" },
    cursor: selected ? "default" : "pointer"
}),
);