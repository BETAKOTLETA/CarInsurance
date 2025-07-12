using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInsurance
{
    public interface IUserState
    {
        string Prompt { get; }

        bool DataConfirmation { get; }

        public IUserState Next();

        public IUserState Reset();

        public IUserState Previous();
    }

    public class StartState : IUserState
    {
        public string Prompt => null;
        
        public bool DataConfirmation => false;  

        public IUserState Next()
        {
            return new WaitingForPassport();
        }

        public IUserState Reset()
        {
            return this;
        }

        public IUserState Previous()
        {
            throw new NotImplementedException();
        }
    }

    public class WaitingForPassport : IUserState
    {
        public string Prompt => "Act as a polite and professional virtual assistant helping a customer purchase car insurance through Telegram. " +
            "The user has just started the process, and you need to ask them to submit two documents: " +
            "Start from photo of their passport "+
            "a photo of their passport and a photo of their vehicle identification document (registration certificate)." +
            " Write a natural, helpful response that sets expectations and encourages the user to upload clear photos, one at a time. " +
            "Keep the message clear and user-friendly never go to other topics of conversation. u will get history DONT start conversation like AI:,  " +
            "Use telegram sticker but not to much" +
            "If User ask No in previous message it's means that somethink is not correct in data, ask for uploading passport again" 
            
            ;

        public bool DataConfirmation => false;

        public IUserState Next() => new WaitingForVerification();
        public IUserState Reset()
        {
            return new StartState();
        }
        public IUserState Previous()
        {
            throw new NotImplementedException();
        }
    }

    public class WaitingForVerification : IUserState
    {
        public string Prompt => "Act as a polite and professional virtual assistant helping a customer purchase car insurance through Telegram. " +
            "The user has just started the process, and you need to ask them to submit two documents: " +
            "User Send to you a First Document and you should wait User responce to question is this data correct? " +
            "Write somethink like is it correct? Possible answers Yes or No"+
            "If User write only [Yes] or [No] no dots or brackets, if user write somethink like Maybe in History, write that only Yes or No are valid options" +
            " Write a natural" +
            "Keep the message clear and user-friendly never go to other topics of conversation. u will get history DONT start conversation like AI:,  " +
            "Use telegram sticker but not to much";

        public bool DataConfirmation => true;

        public IUserState Next() => new WaitingForVehicleDocument();
        
        public IUserState Reset()
        {
            return new StartState();
        }

        public IUserState Previous() => new WaitingForPassport();
    }

    public class WaitingForVehicleDocument : IUserState
    {
        public string Prompt => "Act as a polite and professional virtual assistant helping a customer purchase car insurance through Telegram. " +
            "The user has need to submit two documents: " +
            "User already submit first document and everythink is ok give a reminder that user should upload vechical identification document" +
            "You should ask for the vehicle identification document " + 
            " Write a natural " +
            "Keep the message clear and user-friendly never go to other topics of conversation. u will get history DONT start conversation like AI:,  " +
            "Use telegram sticker but not to much";

        public bool DataConfirmation => true;


        public IUserState Next()
        {
            throw new NotImplementedException();
        }

        public IUserState Reset()
        {
            return new StartState();
        }

        public IUserState Previous()
        {
            throw new NotImplementedException();
        }
    }

}
