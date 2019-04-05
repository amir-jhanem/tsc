import { Contact } from './ticket';

export interface Contact {
  name: string;
  email: string;
}

export interface Ticket {
  id: number; 
  subject: string; 
  body: string; 
  contact: Contact;
}

export interface SaveTicket {
    id: number; 
    subject: string; 
    body: string; 
    contact: Contact;
}