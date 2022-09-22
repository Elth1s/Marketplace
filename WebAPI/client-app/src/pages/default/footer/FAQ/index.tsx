import Box from '@mui/material/Box';
import AccordionDetails from '@mui/material/AccordionDetails';
import Typography from '@mui/material/Typography';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';

import { useTranslation } from 'react-i18next';
import { FC, useState } from 'react';

import { AccordionStyle, AccordionSummaryStyle, TabsStyle, TabStyle } from './styled';

import { info_45, package_45, shopping_bag_45, truck_45 } from '../../../../assets/icons';

interface TabPanelProps {
    children?: React.ReactNode;
    index: number;
    value: number;
}

interface IAccordionItem {
    question: string;
    answer: string | JSX.Element[];
}

function TabPanel(props: TabPanelProps) {
    const { children, value, index, ...other } = props;

    return (
        <div
            role="tabpanel"
            hidden={value !== index}
            id={`simple-tabpanel-${index}`}
            aria-labelledby={`simple-tab-${index}`}
            {...other}
        >
            {value === index && (
                <Box
                    sx={{
                        display: "flex",
                        flexDirection: "column",
                        justifyContent: "center",
                        alignItems: "center",
                        mt: "50px"
                    }}>
                    {children}
                </Box>
            )}
        </div>
    );
}

const AccordionItem: FC<IAccordionItem> = ({ question, answer }) => {
    return (
        <AccordionStyle>
            <AccordionSummaryStyle
                expandIcon={<ExpandMoreIcon />}
                aria-controls="panel1a-content"
                id="panel1a-header"
            >
                <Typography variant="h5">{question}</Typography>
            </AccordionSummaryStyle>
            <AccordionDetails sx={{ padding: "30px 0px 10px" }}>
                <Typography variant="h5">
                    {answer}
                </Typography>
            </AccordionDetails>
        </AccordionStyle>
    )
}

function a11yProps(index: number) {
    return {
        id: `simple-tab-${index}`,
        'aria-controls': `simple-tabpanel-${index}`,
    };
}


const FAQ = () => {
    const { t } = useTranslation();
    const [value, setValue] = useState(0);

    const faqMain = [
        { question: t('pages.faq.main.mallWork.question'), answer: t('pages.faq.main.mallWork.answer').split('\n').map(line => <p>{line}</p>) },
        { question: t('pages.faq.main.orderShipped.question'), answer: t('pages.faq.main.orderShipped.answer') },
        { question: t('pages.faq.main.itemMissing.question'), answer: t('pages.faq.main.itemMissing.answer').split('\n').map(line => <p>{line}</p>) },
        { question: t('pages.faq.main.changeOrder.question'), answer: t('pages.faq.main.changeOrder.answer') },
    ]

    const faqOrder = [
        { question: t('pages.faq.order.orderCancelled.question'), answer: t('pages.faq.order.orderCancelled.answer') },
        { question: t('pages.faq.order.notMyOrder.question'), answer: t('pages.faq.order.notMyOrder.answer') },
    ]

    const faqDeliver = [
        { question: t('pages.faq.delivery.notReceiveOrder.question'), answer: t('pages.faq.delivery.notReceiveOrder.answer').split('\n').map(line => <p>{line}</p>) },
        { question: t('pages.faq.delivery.changeShippingAddress.question'), answer: t('pages.faq.delivery.changeShippingAddress.answer') },
        { question: t('pages.faq.delivery.combineOrders.question'), answer: t('pages.faq.delivery.combineOrders.answer') },
    ]

    const faqReturn = [
        { question: t('pages.faq.return.returnPolicy.question'), answer: t('pages.faq.return.returnPolicy.answer') },
        { question: t('pages.faq.return.exchangeOrder.question'), answer: t('pages.faq.return.exchangeOrder.answer') }
    ]
    const handleChange = (event: React.SyntheticEvent, newValue: number) => {
        setValue(newValue);
    };

    return (
        <>
            <TabsStyle value={value} onChange={handleChange} sx={{ minHeight: "0" }} >
                <TabStyle icon={<img src={info_45} alt="icon info" />} label={t('pages.faq.tabs.main')} {...a11yProps(0)} />
                <TabStyle icon={<img src={shopping_bag_45} alt="icon info" />} label={t('pages.faq.tabs.order')} {...a11yProps(1)} />
                <TabStyle icon={<img src={truck_45} alt="icon shopping bag" />} label={t('pages.faq.tabs.deliver')} {...a11yProps(2)} />
                <TabStyle icon={<img src={package_45} alt="icon package" />} label={t('pages.faq.tabs.return')} {...a11yProps(3)} />
            </TabsStyle>

            <TabPanel value={value} index={0}>
                {faqMain.map((item, index) => (
                    <AccordionItem key={index} question={item.question} answer={item.answer} />
                ))}
            </TabPanel>
            <TabPanel value={value} index={1}>
                {faqOrder.map((item, index) => (
                    <AccordionItem key={index} question={item.question} answer={item.answer} />
                ))}
            </TabPanel>
            <TabPanel value={value} index={2}>
                {faqDeliver.map((item, index) => (
                    <AccordionItem key={index} question={item.question} answer={item.answer} />
                ))}
            </TabPanel>
            <TabPanel value={value} index={3}>
                {faqReturn.map((item, index) => (
                    <AccordionItem key={index} question={item.question} answer={item.answer} />
                ))}
            </TabPanel>
        </>
    );
}

export default FAQ;