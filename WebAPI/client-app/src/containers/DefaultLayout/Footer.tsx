import Box from "@mui/material/Box";
import Container from "@mui/material/Container";
import Grid from "@mui/material/Grid";

import PhoneIcon from '@mui/icons-material/Phone';
import LocationOnIcon from '@mui/icons-material/LocationOn';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';

import FacebookIcon from '@mui/icons-material/Facebook';
import TelegramIcon from '@mui/icons-material/Telegram';
import InstagramIcon from '@mui/icons-material/Instagram';

import { ItemBoxStyled, TitleStyle, BodyStyle, LinkStyle } from "./styled";

const about = [
    { icon: <PhoneIcon sx={{ color: "#FFF", fontSize: "30px", mr: "10px" }} />, text: "+38054 824 67 31" },
    { icon: <LocationOnIcon sx={{ color: "#FFF", fontSize: "30px", mr: "10px" }} />, text: "Ukraine, str. Soborna 27" },
    { icon: <ShoppingCartIcon sx={{ color: "#FFF", fontSize: "30px", mr: "10px" }} />, text: "10:00 - 21:00 (No weekends)" },
]

const links = [
    {
        title: 'Services and services',
        description: ['Service agreements', 'Gift certificates', 'Credit and payment in installments', 'Gift cards'],
    },
    {
        title: 'Partners',
        description: ['Shafa', 'Crafta.ua', 'Vsisvoi.ua', 'Izi.ua'],
    },
    {
        title: 'Assistance',
        description: ['Find an order', 'Guarantee', 'Service centers', 'Frequently asked questions'],
    },
];

const Footer = () => {
    return (
        <Box component="footer" sx={{ backgroundColor: "secondary.main", marginTop: "auto", py: "110px" }}>
            <Container sx={{ maxWidth: { xl: "xl", lg: "lg", md: "md" } }}>
                <Grid container justifyContent="space-between">
                    <Grid item xs="auto">
                        <TitleStyle variant="h4" sx={{ marginBottom: "30px" }}>About us</TitleStyle>
                        <Box sx={{ display: "flex", marginBottom: "45px" }}>
                            <FacebookIcon sx={{ color: "#FFF", fontSize: "30px", mr: "20px" }} />
                            <TelegramIcon sx={{ color: "#FFF", fontSize: "30px", mr: "20px" }} />
                            <InstagramIcon sx={{ color: "#FFF", fontSize: "30px", mr: "20px" }} />
                        </Box>
                        {about.map((item, index) => (
                            <ItemBoxStyled key={index} display="flex" alignItems="center">
                                {item.icon}
                                <BodyStyle variant="h4">{item.text}</BodyStyle>
                            </ItemBoxStyled>
                        ))}
                    </Grid>
                    {links.map((link, index) => (
                        <Grid key={index} xs="auto" >
                            <TitleStyle variant="h4" sx={{ marginBottom: "60px" }}>{link.title}</TitleStyle>
                            {link.description.map((item, index) => (
                                <ItemBoxStyled key={index} display="flex">
                                    <LinkStyle href="#" variant="h4">{item}</LinkStyle>
                                </ItemBoxStyled>
                            ))}
                        </Grid>
                    ))}
                </Grid>
            </Container>
        </Box>
    );
}

export default Footer;