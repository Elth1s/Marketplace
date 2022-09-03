import { styled, Box, PaginationItem, Button } from "@mui/material";




export const BoxCatalogStyle = styled(Box)(({ theme }) => ({
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

export const BoxFilterStyle = styled(Box)(({ theme }) => ({
    [theme.breakpoints.only('xl')]: {
        maxWidth: "300px",
        minWidth: "300px",
    },
    [theme.breakpoints.down('xl')]: {
        maxWidth: "285px",
        minWidth: "285px",
    },
    "&>div:nth-of-type(1)": {
        paddingTop: "0px"
    },
    display: "flex",
    flexDirection: "column",
    marginRight: "15px",
}));

export const BoxProductStyle = styled(Box)(({ theme }) => ({
    display: "flex",
    flexWrap: 'wrap',
    [theme.breakpoints.only('xl')]: {
        "&>*:nth-of-type(4n)": {
            marginRight: "0px"
        }
    },
    [theme.breakpoints.only('lg')]: {
        "&>*:nth-of-type(3n)": {
            marginRight: "0px"
        }
    },
    [theme.breakpoints.only('md')]: {
        "&>*:nth-of-type(2n)": {
            marginRight: "0px"
        }
    },
}));

export const PaginationItemStyle = styled(PaginationItem)(({ theme }) => ({
    borderRadius: "12px",
    "&.Mui-selected": {
        color: "white",
        backgroundColor: `${theme.palette.secondary.main}`,
        "&:hover": {
            backgroundColor: `${theme.palette.secondary.dark}`,
        },
    }
}));

export const ShowMoreButton = styled(Button)(({ theme }) => ({
    color: "inherit",
    fontSize: "24px",
    textTransform: "none",
    "&:hover": {
        background: "transparent"
    },
    "&& .MuiTouchRipple-child": {
        backgroundColor: "transparent"
    },
    "& .MuiButton-startIcon>*:nth-of-type(1)": {
        fontSize: "25px"
    }
}));