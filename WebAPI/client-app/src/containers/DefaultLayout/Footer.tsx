import Box from "@mui/material/Box";
import Container from "@mui/material/Container";
import Grid from "@mui/material/Grid";

import { useTranslation } from "react-i18next";

import { icon_facebook, icon_instagram, icon_telegram, icon_viber } from "../../assets/icons";

import { ItemBoxStyled, TitleStyle, LinkStyle } from "./styled";

const Footer = () => {
    const { t } = useTranslation();

    const links = [
        {
            title: `${t('containers.default.footer.about.title')}`,
            sublink: [
                {
                    title: `${t('containers.default.footer.about.mall')}`,
                    path: "/about"
                },
                {
                    title: `${t('containers.default.footer.about.contact-info')}`,
                    path: "/contact-info"
                }
            ],
        },
        {
            title: `${t('containers.default.footer.partners.title')}`,
            sublink: [
                {
                    title: ` ${t('containers.default.footer.partners.shafa')}`,
                    path: "/shafa",
                },
                {
                    title: `${t('containers.default.footer.partners.crafta')}`,
                    path: "/crafta",
                }
            ],
        },
        {
            title: `${t('containers.default.footer.help.title')}`,
            sublink: [
                {
                    title: `${t('containers.default.footer.help.FAQ')}`,
                    path: "/faq",
                },
            ],
        },
    ];

    return (
        <Box component="footer" sx={{ backgroundColor: "secondary.main", marginTop: "auto", pt: "75px", pb: "110px" }}>
            <Container sx={{ maxWidth: { xl: "xl", lg: "lg", md: "md" } }}>
                <Grid container justifyContent="space-between">
                    <Grid item >
                        <TitleStyle variant="h4" sx={{ marginBottom: "40px" }}>{t('containers.default.footer.join')}</TitleStyle>
                        <Box sx={{ display: "flex", marginBottom: "45px" }}>
                            <img
                                style={{ width: "30px", height: "30px", marginRight: "20px" }}
                                src={icon_facebook}
                                alt="icon"
                            />
                            <img
                                style={{ width: "30px", height: "30px", marginRight: "20px" }}
                                src={icon_viber}
                                alt="icon"
                            />
                            <img
                                style={{ width: "30px", height: "30px", marginRight: "20px" }}
                                src={icon_telegram}
                                alt="icon"
                            />
                            <img
                                style={{ width: "30px", height: "30px", marginRight: "20px" }}
                                src={icon_instagram}
                                alt="icon"
                            />
                        </Box>
                    </Grid>
                    {links.map((link, index) => (
                        <Grid key={index} >
                            <TitleStyle variant="h4" sx={{ marginBottom: "40px" }}>{link.title}</TitleStyle>
                            {link.sublink.map((item, index) => (
                                <ItemBoxStyled key={index} display="flex">
                                    <LinkStyle href={item.path} variant="h4">{item.title}</LinkStyle>
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