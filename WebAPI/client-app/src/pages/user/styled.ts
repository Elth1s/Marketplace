import { Paper, Tab, Button, Box } from "@mui/material";

import { styled } from "@mui/material";

export const BoxStyled = styled(Box)(({ theme }) => ({
    display: "flex",
    flexWrap: 'wrap',
    [theme.breakpoints.only('xl')]: {
        "&>*:nth-of-type(5n)": {
            marginRight: "0px"
        }
    },
    [theme.breakpoints.only("lg")]: {
        "&>*:nth-of-type(4n)": {
            marginRight: "0px"
        }
    },
    [theme.breakpoints.only('md')]: {
        "&>*:nth-of-type(3n)": {
            marginRight: "0px"
        }
    },
}));

export const PaperStyled = styled(Paper)(() => ({
    boxShadow: "0px 5px 15px rgba(0, 0, 0, 0.25)",
    borderRadius: "10px"
}));

export const Img = styled('img')({
    margin: 'auto',
    display: 'block',
    maxWidth: '100%',
    maxHeight: '100%',
});

export const TabStyled = styled(Tab)(({ theme }) => ({
    maxWidth: "none",
    color: "#777777",
    fontSize: "27px",
    fontWeight: "400",
    lineHeight: "34px",
    padding: "5px 0",
    textTransform: "none",
    "&:nth-of-type(1)": {
        marginRight: "300px",
    },
    ".Mui-selected": {
        color: theme.palette.primary.main,
    }
}));

export const ButtonStyled = styled(Button)(() => ({
    fontSize: "20px",
    fontWeight: "700",
    lineHeight: "25px",
    marginTop: "97px",
    padding: "7px 32px",
    textTransform: "none",
}));

export const ChangeButton = styled(Button)(() => ({
    width: "175px",
    height: "32px",
    fontSize: "14px",
    lineHeight: "18px",
    fontWeight: "500",
    borderRadius: "5px",
    textTransform: "none",
    paddingLeft: "0px",
    paddingRight: "0px"
}));

export const BlindButton = styled(Button)(() => ({
    fontSize: "16px",
    lineHeight: "20px",
    fontWeight: "500",
    padding: 0,
    textTransform: "none",
    "&:hover": {
        backgroundColor: "transparent",
    }
}));