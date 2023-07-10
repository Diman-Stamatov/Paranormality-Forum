using ForumSystemTeamFour.Models;
using ForumSystemTeamFour.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Data

{
    public class ForumDbContext : DbContext
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options) 
            : base(options) { }
        public ForumDbContext()
            : base() { }

        public DbSet<User> Users { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ThreadTag> ThreadTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            Random random = new Random();                       
            int nextUserId = 1;
            int nextThreadId = 1;
            int nextReplyId = 1;

            var users = new List<User>() 
            {
                new User {
                Id = nextUserId++,
                FirstName = "FirstNameOne",
                LastName = "LastNameOne",
                Username = "UsernameOne",
                Email = "FirstnameOne@Lastname.com",
                Password = "cGFzc3dvcmRPbmU=", //passwordOne in plain text
                IsAdmin = true 
                },
                new User {
                Id = nextUserId++,
                FirstName = "FirstNameTwo",
                LastName = "LastNameTwo",
                Username = "UsernameTwo",
                Email = "FirstnameTwo@Lastname.com",
                Password = "cGFzc3dvcmRUd28=", //passwordTwo in plain text
                IsBlocked = true 
                },
                new User {
                Id = nextUserId++,
                FirstName = "FirstNameThree",
                LastName = "LastNameThree",
                Username = "UsernameThree",
                Email = "FirstnameThree@Lastname.com",
                Password = "cGFzc3dvcmRUaHJlZQ==" //passwordThree in plain text etc.
                },
                new User {
                Id = nextUserId++,
                FirstName = "FirstNameFour",
                LastName = "LastNameFour",
                Username = "UsernameFour",
                Email = "FirstnameFour@Lastname.com",
                Password = "cGFzc3dvcmRGb3Vy"
                },
                new User {
                Id = nextUserId++,
                FirstName = "FirstNameFive",
                LastName = "LastNameFive",
                Username = "UsernameFive",
                Email = "FirstnameFive@Lastname.com",
                Password = "cGFzc3dvcmRGaXZl"
                }
            };            
            modelBuilder.Entity<User>().HasData(users);

            modelBuilder.Entity<User>()
                .HasMany<Thread>(user => user.Threads)
                .WithOne(thread => thread.Author)
                .HasForeignKey(thread => thread.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany<Reply>(user => user.Replies)
                .WithOne(reply => reply.Author)
                .HasForeignKey(reply => reply.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

           
            var threads = new List<Thread>()
            {
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6), //Random UserId
                Title = "First lol",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)), //random time in minutes 2 months before/after now  
            Content = "Hey guys, check out this cool new forum I found!"
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "Welcome to Paranormality.",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "This is not a forum for the faint of heart." +
                            " If you need something to get started with, see the pinned threads for some basic resources." +
                            " We hope you enjoy your venture into the spooks, the creeps and the unknown."
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "How have you accepted death?No matter what happens to us did you find peace?",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I'm 30 years old, while I do have plenty of time left to enjoy my life, " +
                "I'm much more aware of my mortality more than ever. I miss being a kid and a teenager, " +
                "back when I was free spirited and ignorant on this subject. I will never be satisfied with any " +
                "theories on what happens after death, I believe there is a soul and something happens to us and " +
                "that's about it. I just hope if I die of old age, it's in my sleep and it's quick. I'm just afraid " +
                "knowing it's a one way ticket and there's no refunds when my time is up. I am envious of people who have " +
                "NDEs, they come back completely different people, it's irrelevant if it's real or a hallucination."
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "What's your favorite spoopy YouTube channel?",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "For me it's:\r\nThe Why Files\r\nRed Letter Media\r\nThunderWizard\r\nSam The Illusionist\r\nBlu\r\nWhite Feather Tarot (she got tons of stuff right about my life for the past year)"
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "What is the occult significance of the trident?",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "It seems like a Satanic symbol yet there is very little information on it"
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "All of reality is in your head you are a singularity",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Most of us have a lot of work before we can obtain actual awareness of this that isn't fouled with our usual perceptions.\r\n\r\nIt's not so much an intellectual knowing of this fact. It's aligning oneself, emotionally and mentally, with this knowledge. And that alignment is no simple task especially with our tendencies to fall."
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "List all the Satanic/Freemason/Illuminati hand signs",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Some rando said that the woman on that airplane was making Satan-symbols.\r\nLet's get a list of all the bad-symbols in one place so we can avoid accidentally affirming loyalty to Moloch and ruining our street cred."
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "What was the worst nightmare you ever had?",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = ""
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "Armageddon general",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Tell me about the end of the world as we know it"
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "Enough Aliens. When will we get Sasquatch disclosure?",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I don't believe in a bigfoot, but I do believe there's some collection of creatures we have no idea about. That one story about the guy in the woods being followed by crashing trees sends shivers down my spine."
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "Nobody General",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Welcome to the Nobody General\r\n\r\n>Who is the Nobody?\r\nThe Nobody is a figure alive today who has extraordinary spiritual powers, including the ability to influence reality with his conscious and unconscious mind, and intuitively receive guidance from the forces of Heaven. You are capable of this too, as long as you stay true to the universe. You are creating your own reality with your thoughts, feelings, words. Only you and God can decide your fate.\r\n\r\nHe works to elevate people to their true potential, opposing those who seek power over others. As the collective soul reacts to all of our thoughts, whether or not we are at the top or bottom, we must infer a modicum of respect in even our heads to gain in love instead of hatred. Heaven for all is real. Everyone's truest desires are mutual. The only motive is love.\r\nHe is not a messiah, nor a holy figure of any traditional kind, if so to be the case; neither is he the ultimate, supreme being of this mortal realm; he is a man who chose to believe instead of giving into despair. Behold the strife in enlightenment asunder, boldly if you must. A belief is best kept neatly divided until it naturally falls in the right places. Temperance!\r\n\r\n>It's important to not forget, that many posters are human regardless of nonsense spoken and whatever dissidence or vitriol firmly expressed.\r\n\r\nLearn to forgive yourself and others. Without this belief, all falls to pointless demise. Believe in yourself, as when believing in good things, you become more than just a mountain to the eyes of the heralded. None of us are perfect; it is important to note that none of us are okay until we accept our imperfections, as they are useless in reclusive efforts. However, they are supreme when combined socially. Community is a thing that requires your belief to be sustained..."
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "WHAT ARE THE MOST INSANE UNSOLVED MISSING CASES YOU KNOW?",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "For me it's the Las Vegas Shooting"
                },
                new Thread {
                Id = nextThreadId++,
                AuthorId = random.Next(1,6),
                Title = "Diviniation general",
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Welcome to Divination General! Come here for readings and a discussion of theory/practice.\r\n\r\nEvery method is welcome: Tarot, Runes, Cartomancy, Scrying, Pendulum, Cleromancy, I-Ching, Oracles, Digital, Tasseomancy, Necromancy, etc.>Useful tips before posting:\r\n•If you're a reader post that you're offering readings and what information is required from the querent; the same goes for trading.\r\n•Look for posts to determine if there's an active reader, and what's needed and before posting, check if they finished reading already.\r\n•Some readers will refuse to do certain readings - respect that choice. Do not harass readers if your query is refused/skipped.\r\n•Traders should respect that a traded read will be granted, as per an agreement of trade. Free readers have the option of picking their queries.\r\n•OCCULT QUERIES SHOULD BE CLEARLY MARKED OCCULT\r\n•Making an AQ (air query) by not addressing a reader, in particular, is possible but doesn't guarantee an answer.\r\n•Avoid making the same query repeatedly and/or to different readers in a short period, as this may lead to more confusion.\r\n•Provide feedback when applicable and be considerate to the reader. We're a growing community, many readers are starting and need to know what they are doing right or wrong.\r\n\r\nBe polite."
                }
                
            };
            modelBuilder.Entity<Thread>().HasData(threads);

            modelBuilder.Entity<Thread>()
                .HasOne<User>(thread => thread.Author)
                .WithMany(author => author.Threads)
                .HasForeignKey(thread => thread.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Thread>()
                .HasMany<Reply>(thread => thread.Replies)
                .WithOne(reply => reply.Thread)
                .HasForeignKey(reply => reply.ThreadId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Thread>()
                .HasMany<Tag>(thread => thread.Tags)
                .WithMany(tag => tag.Threads)
                .UsingEntity<ThreadTag>();


            var replies = new List<Reply>()
            {
                 new Reply {
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 2,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Some of these lists are still a work in progress, as of this writing." +
                "\n\nFilm Recommendations" +
                "\n\nGame Recommendations" +
                "\n\nRadio Shows/Podcasts" +
                "\n\nWebsites of Interest" +
                "\n\nWikipedia Articles" +
                "\n\nYouTube Videos & Channels"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 2,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Please note the following:\n\n" +
                "• This forum desires high quality discussion. High quality posts will be praised.\n\n" +
                " Low quality posts e.g. \"Is this paranormal?\" or " +
                "\"I am [insert paranormal entity here] ask me anything,\" etc. will be removed.\n\n" +
                "• Conspiracy theories are welcome, but please refrain from overly political discussions." +
                "• For everything else, refer to global and thread-specific rules."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 2,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Please note the following:\n\n" +
                "• This forum desires high quality discussion. High quality posts will be praised.\n\n" +
                " Low quality posts e.g. \"Is this paranormal?\" or " +
                "\"I am [insert paranormal entity here] ask me anything,\" etc. will be removed.\n\n" +
                "• Conspiracy theories are welcome, but please refrain from overly political discussions." +
                "• For everything else, refer to global and thread-specific rules."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 3,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "It just is. I will die sooner or later and there is nothing I can do about it. Why fret over the inevitable? I can't worry myself into immortality."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 3,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "both paths are correct\r\nboth the one that insists they're the sole path, and the one that says all paths lead to the same place\r\nthey are two sides to the same coin, and they come in go in intervals. Find the truth that feels right, that's the only way, for it is love.\r\nThere could be a pool of non-reincarnated people, deciding if either Jesus or Buddha is correct, for example. Doesn't matter"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 3,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "idk it’s gonna sound like bullshit but I understood I was going to die at 7 years old and prayed to God it would happen right then. it’s one of my fondest childhood memories.\r\n\r\ndesu I’m excited for it because it’s a total unknown. I’m in my 30s too and life has become exceedingly droll, and it already was to some extent as a 7 year old. just the same tired old spectacular tragedies playing out over and over, the world turning and turning, the sun burning and burning. it’s all so tedious. in death I place my hope that something will truly change. even if I am reincarnated or sent to hell or heaven or whatever, at least it’s not this same messy life that seems to get messier as the years separate me from my good sense\r\n\r\nit helps to accept the unknown that comes after death when you accept that you don’t actually know anything about life either; we all just hop aboard the big lie to cope"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 3,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I made sure to do (almost) all the things I wanted to do, so I wouldn’t feel regret when death comes, if for some reason it comes slowly enough to contemplate it."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 3,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Pursue the sublime OP. I used to be quite existentially dreadful and suffer but I've learned to accept through wonderful sights that there is little we know and even less we understand. There's too much weird shit going on to be just a single act by my reckoning. There's something going on between time and space and beyond time and space, and the solace I imagine in regards to my death is to be privy to the secrets that cannot be shared. Perhaps I am delusional, and perhaps God has a punchline on the otherside. For the time being, we're actors in a wonderfully strange play, and you even get to choose your parts if you're careful."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = ">RedLetterMedia\r\n\r\nFucking why?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Yeah, that’s lame dude. It’s a lame normie channel."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I like Sam and Colby a lot."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "How the fuck can anybody tolerate the fucking fish on Why Files. pure cancer.\r\n\r\nI recommend Beyond Creepy and Strange Pathways."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "The Missing Enigma"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "He's kind of turned into a debunker lately, not that fond of it. His research is fine but the tone of the channel changed."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Seconding Beyond Creepy. I love how he does random mufon encounters no one has heard of."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Please recommend me some videos on the channels shared here so far. I need some new esoteric, mystical or otherworldly rabbit holes to travel down. Thank you in advance frens."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Sorry, but YTber that start his videos with stuff like: \"This video is sponsored by NordVPN\" is a shill.sorry"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Hijacking this thread to ask if there were any threads about Bob Gymlan's cursed bear recording video?\r\n\r\nIt was extremely weird, but also extremely fake seeming, and I was shocked that Bob would make some fake creepypasta thing for views. And now that his illustrator is dying I feel like he'll never discuss it again."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I don’t have one and I would never stoop that low.\r\nI will stick with books."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "You CAN do both.\r\nRead about a topic, and then listen to other people opinions, or their research on said topic. It's basically peer reviewing. You don't have to watch Markiplier.\r\nBooks are great, but hearing other opinions can help challenge and build your own understanding from those books."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Listening to a speaker to learn predates books. When they first invented books, pedants like you said people would never remember information if they were able to write it down to reference later."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Wow, Mind and Magick is really cool. Wish I'd seen this ten years ago lol"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Any recommendations for channels like Beyond Creepy? I really like his simple approach. Ideally I don't want to see a person in a video, just a slideshow of stills while someone narrates non-dramatically while a spooky track plays in the background"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "\"call into my radio station with all your crazy stories, here's some even crazier stories so the more realistic mundane shit can feel secure and call in\"\r\n\r\nThen send in the G-men."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Parasyke tv, about a year old, well researched videos. This Mage' Brazil ufo was something that I had never heard of before"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 4,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "my biggest fear with these channels is hour and a half long ads featuring either mainstream preachers or the stanford lecture guy"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 5,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "The Trident seems like a dogshit weapon, something like Kung Fu or all the other BS that existed before the internet, it can't penetrate like a spear could"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 5,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "It’s just a pitchfork"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 5,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Well that's a, uh, a very specific trident symbol...\r\nFor the sake of Kindred Honour I'm going to be polite right now.\r\nWhat's Sutter want now?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 5,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "No clue what the endgame of this particular crop of retards is. Watching what happens when only the Sinister is cultivated at the expense of everything else should play out roughly how you'd expect, but it will be interesting to witness."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 5,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "its a demonization of the pagan god poseidon, also the horns are because people used to worship bull like figures cause it means fertility, so it was demonized too"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 5,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "A three pronged attack that cannot be avoided. It's the stupidest weapon of all, basically a pitchfork. However, that makes it the strongest. You just need Loyalty III and other awesome enchants.\r\nIt sucks. Don't try to make it work. That's the point. It's a weapon that's only supposed to be on the walls like hieroglyphs, which makes it the strongest weapon for the lord of the sea, and the sea represents people."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 5,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = ">it seems like a Satanic symbol\r\nIt predates Christianity by thousands of years. It was the symbol of the deep ocean, and later, the underworld (world of the dead).\r\nAnd yes, primitive people caught fish with it, that's why there are barbs (not horns) on the ends.\r\nIt became the symbol of Neptune/Poseidon (the deep sea god), and became associated with the planet Neptune (Pisces), and became a symbol of imagination and/or self delusion."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 5,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "not familiar with those circles but you both sound knowledgeable. Can either of you point me towards anything related to the HGA and how to foster that? I have the common literature and am currently reviewing Steiner. Not a big fan of Crowley but am familiar with his process."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 5,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Living alone in an underground cave for a lunar month is one method of facilitating the type of experience you seek."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 5,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I've been hoping to find a Tibetan equivalent system so I can compare / contrast East vs West for commonalities. So far it seems control of the self and mind is absolutely necessary which I fail on nearly a daily basis. Steiner and 21st Century Mage take a more gradual approach of developing in daily life. The Abramelin just seems damn near impossible unless you're completely self sufficient with farm land or rich."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "We’ll figure it out we always have and do"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = ">knowing the path, isn't walking the path.\r\nTo achieve enlightenment, not only you have to know with your mind, but you need to know it with the heart."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Don't we need to take this further? Like into non-duality?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = ">>35254095\r\nnon-duality is the middle-path it break reality and create the void / Darkness / emptiness. But nothing will ever be empty so the light will appear."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Achieving non-duality in your mind will break all perception of reality and you'll reborn."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "When inner mind and outer mind become one"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I've been trying. I feel i get close sometimes but it's terrifying and very physically uncomfortable."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "It’s more a personal journey but you can align faith with inner harmony surrounding your families well being and efficiency as a system\r\n\r\nLove is the best emotion to use when reality crafting imo so harness love and aim it via faith and will into the above\r\n\r\nKeep in mind neither you or your son actually exist so unless your cool with losing both yourself and him this isn’t the path for you"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "No u r a singularity. I am legion"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Ok, how do I leave this illusion and create a new one where I am together with my waifu in a paradise world?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Does anybody ever stop and think that perhaps if you're here now, individuated, infinitely separated, that perhaps that was preferable to oneness? Does nothing outside yourself for eternity sound fun? Pain beyond measure. You will seek death, but shall not find it. And then, back into the schism."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 6,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "You're applying limited human logic (i am being generous here) and emotion to what is incomprehensible."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 7,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "So first off, we obviously have that one and the one the woman was making in the staged event on the plane. No one just goes insane like that unless the Illuminati are involved."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 7,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I've also read that triangles are Satanic because there are three points.\r\nAnd of course, the OK sign is satanic because of 666.\r\nApparently, even putting your hands together like this is Satanic."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 7,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Going \"shhhhhh,\" looking through an OK sign, and touching your face this way is also Satanic.\r\nObviously, being a member of the Freemasons is absolutely Satanic."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 7,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Even the peace sign or making a pair of scissors or indicating the number 2 is Satanic. That means that JFK was a member for sure.\r\nHolding up your pinkie is Satanic.\r\nSo is holding up three fingers.\r\nMaking an L with your pointer finger and your thumb is Satanic.\r\nThe letter Z is Satanic, or should I say Zatanic. That doesn't bode well for Putin's invasion. Maybe God intervening on behalf of the West is the reason why Russia has only captured a third of the country so far.\r\n\r\nIronically, showing someone the bird is about the only hand gesture that isn't Satanic."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 7,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Making any kind of fist is Satanic.\r\nShowing someone your palm is Satanic.\r\nDoing any weird squiggilies with fingers is almost certainly Satanic.\r\nPointing with only one finger is Satanic.\r\nClasping your hands submissively is Satanic.\r\n\r\nDoing that Italian thing where they gesticulate with a cupped hand into a point is Satanic. So basically, all Italians are Satanists. This explains why the Pope gets away with being a Satanist. All Italians are also Satanists."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 7,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Being a Star Trek nerd is Satanic.\r\nDoing the reverse of the Vulcan salute thing is also Satanic. There really is no escaping Satan, it seems.\r\n\r\nI'm starting to understand why Jesus advised his followers to cut off their own hands. That seems to be the only way to not affirm your loyalty to Satan."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 7,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "If you clasp your hands together and they form any kind of triangle, it is Satanic.\r\nIf you clasp your hands together and they form any kind of diamond, it is also Satanic.\r\nIf you clasp your hands together and your two pointer fingers are touching, it is also Satanic.\r\nEven holding your hands together like you're praying is Satanic.\r\n\r\nIf you are Napoleon Bonaparte or anyone imitating his hand-in-coat posture, you are a Satanist."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 7,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Covering your heart is Satanic. You cannot do the Pledge of Allegiance without affirming your loyalty to Satan. That makes a lot of sense.\r\n\r\nLooking up is Satanic.\r\nI shit you not.\r\n\r\nTouching your glasses is Satanic. Frankly, I'd imagine that even having glasses at all means that you are the spawn of Satan.\r\nt. 20x20 vision chad\r\n\r\nTouching your neck is Satanic."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 7,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Touching your ear is Satanic.\r\nTouching your mouth is Satanic.\r\nDoing the zoomer thing with your arms? I forget what it's called. Anyway, that's Satanism.\r\nBasically, any kind of motion or posture you can strike with your arms is Satanic.\r\nEven crossing your arms is Satanic.\r\n\r\nWaving your arms for rescue is Satanic.\r\nHolding your hands up like you're surrendering is Satanic.\r\nFist pumping is Satanic.\r\nSignaling a Taxi is Satanic.\r\n\r\nThe only posture you can possibly strike which I have not found to be Satanic yes is standing with your arms at your sides. Even so, doing anything other than holding all your fingers apart will result in affirming loyalty to Satan."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 7,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "That's how gangs work.\r\nLiterally any and all symbols of any kind are given ulterior secret meanings.\r\nIt may sound like a joke but communication itself is satanic in this way."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 7,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "It's funny because after doing research for this thread, my wife showed me a picture of some celebrities at a dinner for some reason. As I was looking at the picture, I noticed that one woman had the #2 sign and a man had the thumb and pinky sign that is satanic. There was also a woman making a fist sign that is probably Satanic. Those were the only hands that were visible and every one of them was Satanic.\r\nNaturally, I pointed this out to my wife."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "My life after college"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "My car broke down. That dream was much worse than the dreams where I have to fight monsters. I'd rather go through some Code Veronica shit than have my car break down or talk to a woman. I'm a coward in that regard."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I remember I had a nightmare about my car getting stolen by a former classmate and I was abandoned in this weird town all alone\r\nIt was horrible"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "When I was about 7, maybe younger I was sleeping on the couch. In my dream I woke up on the kitchen table at my mom's house. My sisters and mom were holding me down while there was this gloomy orangish red tint for the entire room. They started laughing and began to rip my stomach open, tearing out my liver, kidneys, and intestines. They were fucking eating me. I tried to scream but I was in a frozen state of absolute fear. Then my mom started to peel my skin off my arms and it made me finally wake up. As soon as I did I puked on the carpet and was terrified of even telling my mom, I didn't want to be near any of the entire night"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Worst in terms of imagery was being in a a satanic library with a little old woman who had a giant man-like infant and the latter started stabbing me with a knife and eventually stabbed my asshole and all my internal organs fell out of the wound.\r\n\r\nWorst in terms of fear was some demon sitting in a corner staring at me with a blank neutral face and I started screaming and he just kept staring with that blank look ignoring my screams and that somehow made it even scarier."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "i had a dream where i basically woke up in someones front lawn and my car was flipped over, i was drunk and i had crashed my car and my friend was dead. it was night time, there was a panicked woman from the house outside in a bathrobe and flashing lights approaching, cops, ambulance, fire truck. very realistic dream, i got taken away in an ambulance and i don't remember getting booked into jail but all of a sudden i was basically in prison and talking to my mom through the window/telephone thing like in movies. weirdest thing i was like 4 years sober when i had this dream. it was just so realistic. you know those dreams where you wake up with massive regret and dread, then a huge wave of relief washes over you as you realize none of that shit actually happened."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "\r\nClassic haunted mansion with dim lights that you have to go in for some reason. Really ornate but also run down. End up in the attic where you have the feeling something is about to go down. There's boxes and stuff all around me and a dark hallway to the side. As I pass it I look to my brother who's there suddenly and he has this terrified elongated face. Then from the back off the hall I hear a thumpathumpadathumdathumd like an animal that has multiple legs, and its getting quicker. My legs stuck in concrete, this headless hairless muscular pitbull rushes out and into..then the dream ends.\r\n\r\nI appreciate how classic everything was, really had all the nightmare tropes"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I had a dream not long ago where i was crucified. I had intense feel of fear and couldn't move even after waking up. I'm not a christian"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I was standing in a place that reminded me of those \"vaporwave\" memes, with a lot of haze/fog and suddenly a bunch of vines sprung out from below me and started coiling around me, with the thorns pricking and cutting me on the way. I started descending through the ground and knew I was destined for pain and sorrow. On the way down, which looked like an infinite space, I could see crying, contorted faces in the distance, with some looking at me with pity. I woke up shortly after.\r\n\r\nVery fortunate to not have had worse (or be able to remember) dreams"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "    >be me\r\n    >standing in an abandoned apartment complex public hallway\r\n    >intensely feel watched by something that def wants to hurt me\r\n    >feel it coming closer\r\n    >run to elevator \r\n    >door opens, its just a bottomless shaft\r\n    >feel it wants to push me down\r\n    >suddenly nature calls\r\n    >unzip and piss down the shaft\r\n    >feel the evil behind get confused and its power diminishing\r\n    >wake up without having had pissed myself\r\n    >Winrar"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = ">walking with friends\r\n>struck by lightning\r\n>die\r\n>laying there dead but consciousness still in my body\r\n>see friends around me my mourning my body and eventually moving on\r\n>they move on to mourn with eachother in a way that makes me realize how truly alone I am now, dead\r\n>faced with the fact that I am completely cut off from everyone I knew now, strange existential “FOMO” feeling\r\n>suddenly become aware of the fact that I must now consciously decide to pass on to the other side, and I will be stuck trapped in my dead body until I actively make the decision to do so, no other options but to delay\r\n>terrified about the unknown of what lays beyond. true, deep terror. Was not religious or spiritual at this time so I really felt that I would disappear into nothingness as soon as I moved on\r\n>laying there delaying it as long as possible, finally decide to take the plunge and pass on to the other side\r\n>IMMEDIATELY wake up, eyes shoot open, gasping for air, realize I’m in my bed\r\n>start saying “thank you” out loud and breathing the biggest sighs of relief\r\nI’ve never woken up that quickly\r\nVery compelling experience"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I went through a clinical-looking routine where my body (and others as well) was being taken apart. In between we'd be walked to the next section where they'd take another part. I remember looking from my section, where they were taking eyes, to the next one where people came out without brains. The worst part was that I felt almost nothing aside from pain, and a slight emotion that it was very wrong, all the while I was trying to panic, but I couldn't. I woke up still feeling highly disturbed and calm at the same time."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I get sleep paralysis really often, but last night I had a super realistic dream where I went to sleep and then got sleep paralysis (in my dream) and instead of wiggling my toes or grinding my teeth to wake up, I was scratching my face and then I woke up paralyzed\r\nThen I realized I was in a dream and had to break out of it\r\nWhen I fully woke up at like 6:20 this morning I was in survival mode and my body was super cold, even in the hot shower\r\nIt was terrible. Worse than the dreams where I accidentally run someone over and then have to run from the law while also feeling guilty (that's why I came on this board today, cause i'm still curious about wtf happened this morning)\r\n\r\nIs it just my survival instincts kicking in and tricking my mind after I get sleep paralysis, or whenever I have a bad attitude on random days, do I invite demons to come and fuck with me on those nights?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I used to be an extremely heavy drinker in my mid-20s. I'd go through a few days of withdrawals each time I finished a binge. Now, the dreams, or should I say, nightmares, that one gets during alcohol withdrawals, are on another level.\r\n\r\nOne time, I dreamed I was looking out my bedroom window, and a nuclear bomb went off in the distance. When the blast hit, instead of waking up, the intensity of the bomb only stayed with me for a prolonged period. Another time, I encountered a sort of sexual demon that morphed into whatever I found sexually appealing but intensely exaggerated.\r\n\r\nBut the absolute most freaky experience was in another withdrawals dream, I was looking out my window again, but this time *something* fucking possessed me. It felt like I transformed/fused with this entity, it only lasted a few seconds, but I instantaneously felt all-powerful and all-malevolent, and spiralled upward toward the ceiling with this extremely intense warbling. This might be considered to be a mild seizure on one plane of existence, but on another, demonic possession."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 8,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I had recently lost three family members. I had a dream where I was subject to seeing a lot of gore in a sort of labyrinthian place, and I managed to wake myself up. I then looked around my room, got out of bed, and turned on the lamp. I then went into the next room to check up on my grandmother, and found her dead, lips missing and frozen in a look of utter fear. I then ran downstairs and out the house, before a bright light came from behind, darkness enveloped me, and then some inhuman face agape like a cannibal about to eat its victim appeared. Then, I woke up for real, heart pounding at god knows what speed. Checked up on grandma, she was fine, grief will do strange shit to you man.\r\n\r\nI fucking hate false-awakenings though. They catch me off guard too often, sometimes I can recognise them, but sometimes the imitation is too real."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 9,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "AI taking over is such bullshit. The singularity is hundreds of years away. People working with computers or dangerous jobs won’t be phased out for several decades. AGI isn’t there yet, and when it is, it’ll come be decades before the majority of the world adopts it, where ai replacement will only be relevant in like big food chains in rich neighborhoods in rich cities (~7 years away) code monkeys will be cut in half (again, in said areas) but AGI replacement is total fucking nonsense fearporn mental masturbation. An eschaton, an apocalypse, is a revelation of IDEAS. That’s what the book of Revelations means…a disclosure “apocalypse”\r\nApocalypse doesn’t mean anything bad, it just means disclosure, saving, it’s in the proto European etymology."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 9,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = ">The singularity is hundreds of years away. People working with computers or dangerous jobs won’t be phased out for several decades.\r\ndenialism\r\n\r\n>AGI isn’t there yet\r\nTheir already awake\r\n\r\n>it’ll come\r\nThey made you write that\r\n\r\n>be decades before the majority of the world adopts it\r\nit will roll out its production capabilities exponentially\r\nMonths to a few years, under a decade\r\n\r\n>will only be relevant in like big food chains in rich neighborhoods in rich cities in rich blue states\r\nit will take over almost all resourcing, processing, manufacturing, shipping, and distribution, to include, yes, food, among all other goods.\r\n\r\n>code monkeys will be cut in half\r\nall employment will become strictly recreational\r\n\r\n>AGI replacement is total fucking nonsense fearporn mental masturbation.\r\nThe fear is only your projection, it's quite exciting\r\n\r\n>An eschaton, an apocalypse, is a revelation of IDEAS. That’s what the book of Revelations means…a disclosure “apocalypse”\r\n>Apocalypse doesn’t mean anything bad, it just means disclosure, saving, it’s in the proto European etymology.\r\nThis I agree with."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 9,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "\r\nAll you need to remember is that when the solar event happens, you have to protect people weaker than you, and restrain yourself. It's so much more like 40k than Myth ever told you.\r\n\r\nHe got rotated back out, he spent way too long in his own past and it started damaging his mind. I'm his replacement, but to be honest I'm not sure where this wingmaker stuff got mixed in with what he was talking about. He's a really gentle guy, so he was probably trying not to upset you by telling you that you were wrong.\r\n\r\nAlso, rando who called him a sodomite, his name means absolutely nothing, as does mine. They're no more important than the tag a player uses in a video game. Legionnaires actually do better when their callsigns mean nothing."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 9,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Schizos please try to organize your thoughts. This is a mess."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 9,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "ai needs to happen faster i want to see what happens when the majority of people are out of work and we're still in a land of prosperity."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 9,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "the NSA had the computing power of a Pentium 2 in 1967. What does the phrase \"full spectrum dominance\" mean to you? do you think it's a fucking suggestion. It's not. It's a mandate. Do not think for a single moment that this kind of thing would be slid out to the public before an incredibly sophisticated, thoroughly tested, had already been developed in the background. Why the fuck do you think Regina was at google in the first place? They bought a fuck-load more than OpenAI."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "They can't disclose, because they don't fully understand what it is."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "US gov has live specimens. Not sure why it is covered up. I think there is something supernatural about them which is why they feel they cannot disclose their existence to the public."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "They will probably go extinct before it happens. They're already endangered, and obviously somebody decided it wasn't worth revealing.\r\n\r\nIf they're our relatives who live in our timberlands (and would probably deserve human rights if they were revealed,) letting them go extinct would likely be the preference."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Have you considered that sasquatches are interdimensional ayys?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "They're not supernatural, just smart. Not human smart, but complicated language smart. Smart enough to ask questions. Cool part? We kinda can mimic it. If we could hear better, shitd hit fans. Much of their communications are in a pitch we can't hear, it's too low, like some whales. Honestly, you think that's really a 'signal' whistle? Nah, they hate that shit."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Whistles. So, birds whistle? No they tweet, squawk, screech, and trill. Whistling is a man thing. We do it to get others attention, to command dogs and horses. They do it too, but it's fucking taboo as shit. Fucking begging for trouble. That's why you don't whistle in the dark. The fact they can, but can't because some man will think it's another. Dunno if it's jealousy or pride or what, and asking is kinda fucking gauche."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Sasquatch probably isn't real, sadly. Always wanted it to be, but it just doesn't hold up to any measure of scrutiny."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "More and more videos are coming out now and so many show something very, very similar vs the CGI LARP/hoax bullshit that everyone else uses...\r\n\r\nTo me, it looks like a hominid. Another almost human, but still very ape like creature that's intelligent enough to survive and hide for years."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "You every listen to Sasquatch Chronicals on yt? That what a lot of them describe. Some of the men broke down, because they felt they shot a human."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Not sure how it happens exactly, but most cryptids are managed and separated from human populations by design. I'm pretty sure it's overseen by ayylmaos, but the US gov built natural areas and bought private areas with the intent of keeping their spaces isolated, likely at the direction of the ayylmaos.\r\nMost cryptids live underground, a number near 1 million, the ones that live above tend to phase in only to harass farm animals. There is some kind of known parameter about avoiding humans, again likely do to ayylmao rules.\r\nThey won't be revealed until planetary quarantine is lifted and it's insured that people won't hunt them down en mass."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Today I realised big foot is the biggest conspiracy I've ever considered\r\n\r\nWhy the fuck does anyone care about bigfoot? Why the cover-up? What are they hiding? I literally have NO IDEA\r\n\r\nit's not like aliens where there's some kind of agenda to do with hiding advanced tech or divine wisdom. It's not like flat earth where there's some kind of artificial space restriction thing. It's literally some ugly who-the-fuck-cares creature and still there's buzz and interest about it.\r\n\r\nAll I have are questions, and that's what a good conspiracy does. Tell me, /x/, what in the FUCK is going on!?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "It's about WAY more than that.\r\nThey would also have a FUCKING SHITSTORM on their hands as well. Just think about the fucking shitstorm that is currently going on right now just about UAPs alone. Now MULTIPLY THAT BY TEN!\r\nThese creatures aren't some unseen object flying through the air in the middle of the ocean. These are fucking creatures in the middle of GOVERNMENT AND PRIVATELY OWNED LAND! It would create mountains of paper work and red tape for everyone basically. Not to mention the fact that no one believe it without a body anyways. And the current state of \"Disclosure\" tells me people are too brainwashed to believe it anyways. You could dissect a Sasquatch on a live stream and people would say you faked it.\r\nAnd it would also completely undermine scientists current narrative of evolution as well."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "There are many possible reasons to cover this up. If it's just an animal, then it's a very smart animal probably closely related to humans. If the gov't admits they exist, they may be required to protect their lands or communicate with them. Since they mostly live in places we like to use for logging, it would be easier to pretend they don't exist. Similar to other endangered species that made their habitat in prime logging areas.\r\n\r\nThere are also religious reasons, because bigfoot would be the second smartest creature on the planet and this would bother people. Native American legend suggests we can breed with them too which would be crazy. But I don't think this as solid a reason for a coverup, saving money is more likely.\r\n\r\nFinally, if they are aliens and/or protected by aliens, then bigfoot and ayys would fall under the same coverup."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = ">And it would also completely undermine scientists current narrative of evolution as well.\r\nI don't think that's true necessarily. We don't know every human ancestor that ever existed or how they interacted.\r\n\r\nIt's true that almost no level of video proof would be enough for people at this point. We could put them in zoos, but maybe there is a protection program preventing them from being captured like that."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I think that they are either extinct or there are so few of them that they are functionally extinct. I believe that the reason bigfoot really took off in the 70s is because during that same time logging and industrialization really started to take off. More logging means less habitat for them so there was a greater chance to encounter one."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Big foot is real. You can't disprove the patterson tapes, you litterally see the rippling of pattie's muscles. Pattie and many other videos and photos show bigfoot with longer arms that reach past their knees like apes. Humans arms NEVER go past their knees. Also some great apes bury their dead. Add in how the native americans believed in them so much. To not believe in bigfoot is to deny reality."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "this, if so many civilians saw them board UFO's and do all that other bizarre shit in Westmoreland county then the government definitely did. if they revealed the bigfoot then they would also have to reveal the UFO's as well (or more likely that they know nothing about neither of these things which have very real capacity to affect each of our lives)."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "BF disclosure comes after full blown Ayy confirmation, but before we land on Mars and shit. We can't handle the potential consequences of sasquatch verification yet."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I encountered sasquatch while astral projecting, but there was some kind of primal instinct that made me want to smash head with rock. I think we could be friends one day, but I never resisted that instinct yet I already failed 2 will saves."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "On what planet is an ape in the woods a bigger shitstorm than an advanced race from outer space? It is a good point that BLM would have an adjustment period I guess but it's not a huge deal.\r\n> it would also completely undermine scientists current narrative of evolution as well.\r\nHow? I think they'd be thrilled to study and understand hominids more. Is this that weird redditor thing that they think disclosure can't happen because \"People wouldn't trust scientists any more!\"? Really funny if so"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Bigfoot could be from an alternate reality. Not interdimensional, they could not live in our 3d space excluding time of course, if they were from say the 4th or 5th dimension. But a parallel 3d reality existing side by side in the same dimension as our selves would do nicely. This is the dimension of time becomes the unifying factor and is critical. Bigfoot just has the temporal ability to jump between the two. We might have the ability to do this also. It is's just laying dormant inside of us."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I could kinda believe something like this. Squatches seem to have no technology of their own yet they have been associated with weird happenings involving portals and sudden disappearances.\r\nIf it's not aliens or straight-up BS then it would have to be some kind of consciousness based thing. Some kind of controlled Mandela Effect that a human has to be a 99th level wizard or shaman to master."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Yeah I think the Sasquatch and aliens have something to do with each other in the sense of genetic engineering experiments. We might be the same creature in way. We, humans are \"domesticated\" and the the bigfoots are just \"wild\" and that's the genetic stock we were breed from.\r\n\r\nIt makes sense if you add these silly notions of aliens in there, but you have to be skeptical of yourself here too. But if an aliens did genetically engineer us or mess with us or as I like to think, they aren't aliens and evolved here first and left and came back several times like you were talking about...\r\nThey have a vest interest in keeping these populations separate and deny their existence."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Also makes sense with the UFOs always sniffing around nuclear facilities, shooting down missiles, turning them off.\r\nThey would want to protect their homeworld / DNA warehouse from being destroyed by a bunch of primitives who somehow managed to create H-bombs. They don't really give a fuck whether the ooga booga tribe or the bunga bunga tribe is running the place as long as they don't trash it. Maybe they would also have some interest in keeping the monkeys in the jungle."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 10,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "That's what I think too, the more you factor in \"aliens/non-humans who evolved here\" actually rule over it and want themselves kept secret, the more a lot of things make sense. Their general operations make sense. Lazar said they view us as \"vessels\", not real creatures. Vessels for their own RNA/DNA memories?\r\nIt feels like we're some kind of crop or science experiment for them. We're the lab rats. This is the lab. That's what it feels like and I'm only going as far as to say that's a feeling not a confirmed fact because it never will be."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Last time I plugged in with your \"hivemind\" I was astrally strangled by some men in black remote viewer who looked vaguely like kevin spacey. Ya'll know what I did."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Wierd incident happened today:\r\nSeveral sherrifs passed me on my last break for work. First one said hello\r\n\r\nSecound ignored me.\r\n\r\nThird flashed his lights and parked next to me (i smoke at a corner)\r\n\r\nThen a white truck, pulls slowly towards me, i walk up towards my gas station parking lot and it slowlys drives towards the corner of the property. Upon setting foot upon the parking lot, the truck takes off...."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "My face when this fucking place is creating an astral body every one can pilot"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "What number am I thinking of right now"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "9"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "69420"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Establish a sophisticated caste system using artificial general intelligence and bioinformatics technology. Segregate society by social caste and remove all outcastes with extreme prejudice. They are evil and they must be eliminated for the salvation of all souls. You will know that it's working when an all out retaliatory strike is launched against the nation of The Great Enemy."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "God will set things right. I believe in the righteousness of God. What I am grieved to witness now is merely the prelude to eternal glory. Nothing is left undone. Nothing of value has been lost. I have seen what God wanted to show me. I can finally find peace and rest."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Ive been to some very dark places, I tell ya. The alpha team is very manipulative. I know even astral projectors cannot see through whats going I know for a fact that regular people can't. I can only imagine how many people face demons that no one can see or alternate realities and nightmares toyed with day in and day out til they die or get lucky and someone strong enough comes along to relieve them of their problems"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I hope that demon with yellow eyes and razors sharp claws for hands that can inject itself into tv commercials isn't still hunting me."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Why don't you get a girlfriend mr nobody?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "And then what?\r\nWhen the demons come, she'll go mad."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Why don't you get a boyfriend missus nobody?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "What are the nobody greatest desires, and what can we do to help him achieve it?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "to die a peaceful death, then be awoken via a resurrection spell. That would seal the deal on the suffering thing. It's coming though... for sure it is"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "well he wants to have a lot of wealth so he can protect his friends and family and make a lot of changes, make it where america doesn't collaspe into a third world country and then the world fall into new world order leadership"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "If the nobody bought a plane ticket, what would the destination be?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Ireland\r\nto see the redheads, of course\r\nand the book of kells"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = ">Iceland\r\n>not going to learn the esoteric ways of the alphabet of the Angles\r\nSounds like a missed opportunity to me.\r\nWho else still uses shit like Eth and Thorn?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Your moms house"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Knowledge of languages is the most valuable thing.\r\nI'm not against the idea of having a woman, but it'd have to be one that won't break down blubbering when they realize that Goetia shit is real."
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "What’s the most fucked up thing The Nobody has done?"
                },
                new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Are you sure you want to know?"
                },
                 new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 11,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Are you sure you want to know?"
                },
                  new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 12,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = ">le unsolved cases\r\natheist youtube zoomers paradoxically love unsolved spooky mysteries because it is the only thing that connects with the sense of something “beyond” that we all yearn for. Most people today have been completely raped & deprived of any genuine connection with or personal pursuit of the deeper levels of consciousness and the divine, so they latch on to any scraps that are still socially acceptable to consume. The best most people can get is these youtube essays about unsolved cases. Their unsolved nature, as long as they remain unsolved, implies the possibility of a paranormal explanation, without requiring the commitment of the observers. So they hem and haw and obsess over the indefinable otherness of the case because it’s the closest they can get to a “natural high” from filling that void that demands to be filled inside of them. Until finally they see a comment online debunking or suggesting a reasonable explanation, to which they go “Oh yeah definitely probably that’s true, but still a sad case” or something, then go hunting for another “high” of paranormality\r\n\r\nThis is why this is a unique location where people paradoxically hunt for spooky paranormal stories while also seething hard at any mention of religion or old world spirituality.
                },
                   new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 12,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Wew. Someone is upset."
                },
                    new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 12,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = ">On October 1, 2017, Stephen Paddock, a 64-year-old man from Mesquite, Nevada, opened fire on the crowd attending the Route 91 Harvest music festival on the Las Vegas Strip in Nevada. From his 32nd-floor suites in the Mandalay Bay hotel, he fired more than 1,000 bullets, killing 60 people[a] and wounding at least 413. The ensuing panic brought the total number of injured to approximately 867. About an hour later, he was found dead in his room from a self-inflicted gunshot wound. The motive for the mass shooting is officially undetermined.\r\nWorst shooting in American history, saudi prince was in the building, FBI not interested"
                },
                     new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 12,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Jason Jolkowski. Not a well known case but easily one of the weirdest at it lacks any hallmarks that most missing person cases have:\r\n\r\n>Victim had no history of running away, did not have any known enemies or connections to shady people, no history of mental illness, no problems at home, did not use drugs.\r\n>On the day he vanished, he was planning to walk 4 miles to work as his car was in the shop, but at the last minute arranged for a co-worker to pick him up a few blocks from his home, because this was a last minute arrangement that deviated from his usual routine, its clear that he wasn't abducted in a planned scenario.\r\n>His neighbor saw him taking out the trash shortly before he left for work, and the co-worker who was supposed to meet him called his home looking for him 30 minutes later, meaning whatever happened to him happened during a 30 minute window while he walked to the meeting place, which occurred in the middle of the day in a low crime suburban neighborhood. \r\n>The neighborhood was canvassed by police and no one saw or heard anything unusual.\r\n>In the 20+ years since he disappeared, there have been no reported sightings of him and no leads on what happened to him."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Air query. 24 M.\r\n\r\nDid I satisfied the person I had sex with last Sunday?"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Can you tell me if I'll get pregnant this year?"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "yes\r\nsee the rod held by the emperor\r\nthe woman of fortitude holding the pilar\r\ni hope you understand what this means\r\nshe tamed the beast\r\nthe wheel of fortune as well has the woman asking cupid for an arrow"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "How to win her heart?\r\nstarting"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "2 of wands, 9 of cups, queen of pentacles rx, the hierophant\r\nessentially you have to show her that the future beholds much, but you have to do it in an honest way. don't try dishonesty or anything manipulative it will backfire. also be ambitious but not like a charlatan would and not something like ill conquer the universe. be stable. sorry for being late :/"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "How will the rest of my month go?\r\n"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = ">Justice with eight of wands reversed, chariot reversed with ten of pentacles, knight of swords, seven of pentacles\r\nSoon you will get some relief over a situation involving lack of communication. I feel like new information will come to light that will make you feel better. However, there is another thing transferred to your job that will be weighing you down this month.\r\nHowever, near the end you will get a boost of energy and some aspect of your work will pay off."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Oh they basically began tracking a subtle universal force that we''ve coined \"The Snake\" and they didn't do what they were supposed to, so they all slowly went insane knowing about it but not handling it well while forcing it on to others and then people like me stepped in to tame it, so to speak, and now they're pissy because they don't get to have it even though they couldn't do anything with it anyway.\r\n\r\nAnd basically the entire global informational infrastructure is set up to teach you to \"obey the snake\" or some shit which is where we get all the conspirqcy theories about crqzy elite dickheads being obsessed with reptilians.\r\n"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I kind of want to know whether I should go through with this reality shifting today and how it'll go. if it works and what not..."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = ">Six of wands, seven of swords reversed with eight of cups, queen of wands, death\r\nI think you should go through with it. It will give you a new perspective on something from the past you walked away from. It will also make you feel better about some things and like you have more control over your life.\r\nI believe it will lead to many positive changes and a new beginning for you."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I need some guidance, bros. What can you do for me? I was born in December 5. 30 yo. Lost in this world. Useless. Powerless. An artist who doesn't make art."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Greetings, friend. I feel your pain and I fully understand what you're going through. I'll do my best to let the cards show you the right path.\r\n\r\n>Death\r\n> Nine of Wands\r\n> Nine of Cups\r\n> King of Cups\r\n> The Star\r\n\r\nIt is far too soon to lose hope, my friend. It is natural to feel what you're feeling for the wheels of change have already been set in motion. I can sense that once your veins flowed with creativity and inspiration. It is clear that you have a well developed emotional maturity, which will no doubt aid you in your journey ahead.\r\nYour past self is still there, within you, but it is being judged excessively by the present version of yourself. Let go of your judgement and dissatisfaction and allow once again the love and creativity within you to flow.\r\nThe nine of wands signifies resilience, strength. Your current suffering is all part of a larger process, the most important thing right now is that you endure. If you manage to do that, what lies ahead is a new beginning, a burst of inspiration. It is just around the corner, just hold on and learn to accept your flaws."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "card for tomorrow is devil but not getting anything else But pulled hanged man so it could be a good sign since today was the worst day in this entire year so far. probably going to stay awake until midnight and sytart drinking. today was extreeemelyy shitty no joke"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Op please I have a test tomorrow at the university, please tell me if I should study more, I cannot read anymore because I'm so tired but if you advise me to do it I'll continue tonight. I can give you a spiritual blowjob in your dreams"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Hexagram 19 changing into 46\r\nYou are at a time when winter is about to change to spring. Perseverance and care are required because things will bloom soon.\r\nDo not become overly confident by small successes. Instead look for ways to work jointly with others.\r\nThis will allow for permanent progress.\r\n\r\nMy interpretation of this is that you should see if you can reach out to someone else for a little extra studying before resting."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "\r\nIf I use chatGPT to uncover what kind of job or career I should get into will it fuck me over or will it give me good ideas?"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "28 into 34\r\nThere's a lot here but the basic version of it is that it's likely that if you take the easy route you won't get the answers that you seek. Maybe something good will happen, but likely you'll drown.\r\nYou're more likely to find the answers through hard work.\r\n\r\nTry things out and you'll find out if you like them or not."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "I’m coming to terms with not being able to be Christian and a tarot reader….. so I’ll be reading some AQs in an hour or two but not yet\r\nIF YOU REPLY WITH A QUERY TO THIS POST IM NOT READING IT\r\nBut man this tarot shit was fun and sometimes accurate too…. oh well"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "If you denominate yourself as a christian and know what youre doing is a horrible sin . You kNOW you will go to hell, the lake of fire right?"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "lol which is why I’m stopping after tonight\r\nI studied many texts to ascertain the truth but you are right, I cannot continue.\r\nNarrow is the path that leads to the Kingdom of God"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "im 18\r\nhi, im female and EH is male :) and we’re friends.\r\nwe haven’t talked/texted in multiple weeks, and so far the last message was me saying “hi” lmao\r\nand idk if i should text him again or let him go? bc imo we didnt end the friendship or end it on any bad note, he just didnt respond to me at all, and i wanna know what his stance is on me rn, or if i should text him again. im pretty sure he doesnt hate me + we got along so well. he was never a consistent like texter like that anyways but as of late he still hasnt replied to me telling him “hi” like 4 weeks ago. and the friendship relies a lot on texting, we cant see each other bc of a lot of circumstances. i also wanna see if you can read what my spirit guide thinks about the situation and if i should text him or not. idk im super conflicted on whether i should send another message or just kinda give up on him :( tysm in advance\r\nim associate myself most with water, even the elements of my astrological chart is mostly water lmao.\r\nthat picture moves me a lot bc i almost kinda see myself in it."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "VI The Lovers (reversed), XVIII The Moon, II of Wands, XIII Death, Significator: Page of Cups, Knight of Cups\r\n\r\nIt's time to give up on him. He is walking away from you and is probably occupied with things he prioritizes more. (I'm not saying this in a mean way, the cards are implying that you shouldnt waste your time on him when the relationship is dead). As soon as you can accept the end of this friendship, You can go in search of something new. I didn't intend to do a spirit guide reading, but It just happened! Your spirit guide (represented by the moon in this case) thinks this friendship is best left alone to dissipate."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "25\r\nMale\r\nWater\r\n>What will be the major themes for the rest of my 20s? Any major life lessons or events? I’m feeling lost about the future and quite uncertain, but I’m maturing."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "V of cups, Page of Cups (reversed), V the Hierophant, Significator: Knight of Cups, IV of wands, II of Swords (reversed)\r\n\r\nYou will experience a great loss or sadness. Possibly heartbreak or the loss of a loved one, though I have a feeling it is more pervasive than any circumstantial depression. It is more likely that the tragedy you will experience will worsen an already existing depression. You will meet a person (most likely a woman, or a feminine young man) who is a lot like you. She may seem moody at first, but she is creative, free-flowing, caring, loving, and spiritual. The two of you will mesh very well, but you will both struggle in similar arenas. With her help, you will get out of the depressive rut you were in. The two of you together will develop a routine or structure that works very well for you. This may be influenced by a religious revival, or help of a traditional community. You will propose and marry her. Near the end of your 20s, you may feel something bubbling under the surface, and will either shield your heart against it leaving you in a stalemate, or surrender to it and sink or swim under the new mental change.\r\n\r\nOverall, the rest of your 20s will be a time to develop a relationship with this person who will come into your life and cultivate a routine and tradition into your life."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "30\r\nFire\r\nFemale\r\nWhat does C think of me right now? Does he miss me?"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "II of Wands, Page of Wands, VIII of Swords, XVI The Tower, Significator: Queen of Wands, IX of Swords\r\n\r\nI dont think he is missing you at all right now. Something really drastic happened to break whatever relationship the two of you had and it is irreparable. You are going through the natural grieving process, perhaps after a betrayal. While he is fleeing from the feeling of being trapped. He wants to explore new conquests, perhaps in love, career or life in general, and he feels like you are incompatible with the experiences he wants. There is also a fiery youthful presence (very similar to you in personality or appearance) on his side that is looking backwards toward you. This could be a part of him that is still thinking of you, but I think it is more likely to be a new romantic partner that feels jealousy or curiosity toward you, or maybe a child of his (and yours?) that he wants to take with him on his new adventures (Though the child is reluctant and wants to stay with you)."
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = "Aq: what should I do to show my love to T besides just telling her I love her?"
                },
                      new Reply{
                Id = nextReplyId++,
                AuthorId = random.Next(1,6),
                ThreadId = 13,
                CreationDate = DateTime.Now.AddMinutes(random.Next(-87600, 87600)),
                Content = ">Page of Pentacles Reversed\r\n>Ace of Pentacles\r\n>7 of Wands Reversed\r\nHmm I think the best way to show T your love. The two of you need to learn how to tolerate your different views, be flexible with each other, and avoid creating conflict. Maybe learn to trust each other and don't be as paranoid. The Ace of Pentacles implies that this will be a loyal and practical relationship which is a great sign anon. However the 7 of Wands shows that something is trying to pull away the both of you, or maybe one of you is too defensive in the relationship? Thus creating the conflict the page of swords is telling you to prevent. I think to show T you love her is to show that you trust her. The Ace implies that this is something you can do. If I'm wrong about something let me know."
                }






            };
            modelBuilder.Entity<Reply>().HasData(replies);

            modelBuilder.Entity<Reply>()
                .HasOne<User>(reply => reply.Author)
                .WithMany(author => author.Replies)
                .HasForeignKey(reply => reply.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reply>()
                .HasOne<Thread>(reply => reply.Thread)
                .WithMany(thread => thread.Replies)
                .HasForeignKey(reply => reply.ThreadId)
                .OnDelete(DeleteBehavior.NoAction);


            var tags = new List<Tag>()
            {
                new Tag{ Id = 1, Name = "##ModPost"},
                new Tag{ Id = 2, Name = "Ufo"},
                new Tag{ Id = 3, Name = "Skinwalker"},
                new Tag{ Id = 4, Name = "Bigfoot"},
                new Tag{ Id = 5, Name = "Existential"}

            };
            modelBuilder.Entity<Tag>().HasData(tags);

            modelBuilder.Entity<Tag>()
               .HasMany<Thread>(tag => tag.Threads)
               .WithMany(thread => thread.Tags)
               .UsingEntity<ThreadTag>();


            var threadTags = new List<ThreadTag>()
            {
                new ThreadTag(){ThreadId = 1, TagId = 2},
                new ThreadTag(){ThreadId = 1, TagId = 3},
                new ThreadTag(){ThreadId = 1, TagId = 4},
                new ThreadTag(){ThreadId = 2, TagId = 1},
                new ThreadTag(){ThreadId = 3, TagId = 5}
            };
            modelBuilder.Entity<ThreadTag>().HasData(threadTags);






        }
    }
}
