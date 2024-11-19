var BP_Confirm = {
	"are_you_sure": "Are you sure?"
};

var BP_DTheme = {
	"accepted": "Accepted",
	"close": "Close",
	"comments": "comments",
	"leave_group_confirm": "Are you sure you want to leave this group?",
	"mark_as_fav": "Favorite",
	"my_favs": "My Favorites",
	"rejected": "Rejected",
	"remove_fav": "Remove Favorite",
	"show_all": "Show all",
	"show_all_comments": "Show all comments for this thread",
	"show_x_comments": "Show all %d comments",
	"unsaved_changes": "Your profile has unsaved changes. If you leave the page, the changes will be lost.",
	"view": "View"
};

var ajaxurl = 'http://alliance.themerex.net/wp-admin/admin-ajax.php';

var my_timeline_front_ajax_nonce = "1eb87fb9eb";
var my_timeline_front_ajax_url = "http://alliance.themerex.net/wp-admin/admin-ajax.php";

var wpurl = "http:\/\/alliance.themerex.net";
var ajaxurl = "http:\/\/alliance.themerex.net\/wp-admin\/admin-ajax.php";


jQuery.noConflict();
jQuery(function() {
	"use strict";
	jQuery(document).foundation();
});


jQuery(document).ready(function() {
	"use strict";
	THEMEREX_GLOBALS["strings"] = {
		bookmark_add: "Add the bookmark",
		bookmark_added: "Current page has been successfully added to the bookmarks. You can see it in the right panel on the tab \'Bookmarks\'",
		bookmark_del: "Delete this bookmark",
		bookmark_title: "Enter bookmark title",
		bookmark_exists: "Current page already exists in the bookmarks list",
		search_error: "Error occurs in AJAX search! Please, type your query and press search icon for the traditional search way.",
		email_confirm: "On the e-mail address <b>%s</b> we sent a confirmation email.<br>Please, open it and click on the link.",
		reviews_vote: "Thanks for your vote! New average rating is:",
		reviews_error: "Error saving your vote! Please, try again later.",
		error_like: "Error saving your like! Please, try again later.",
		error_global: "Global error text",
		name_empty: "The name can\'t be empty",
		name_long: "Too long name",
		email_empty: "Too short (or empty) email address",
		email_long: "Too long email address",
		email_not_valid: "Invalid email address",
		subject_empty: "The subject can\'t be empty",
		subject_long: "Too long subject",
		text_empty: "The message text can\'t be empty",
		text_long: "Too long message text",
		send_complete: "Send message complete!",
		send_error: "Transmit failed!",
		login_empty: "The Login field can\'t be empty",
		login_long: "Too long login field",
		login_success: "Login success! The page will be reloaded in 3 sec.",
		login_failed: "Login failed!",
		password_empty: "The password can\'t be empty and shorter then 4 characters",
		password_long: "Too long password",
		password_not_equal: "The passwords in both fields are not equal",
		registration_success: "Registration success! Please log in!",
		registration_failed: "Registration failed!",
		geocode_error: "Geocode was not successful for the following reason:",
		googlemap_not_avail: "Google map API not available!",
		editor_save_success: "Post content saved!",
		editor_save_error: "Error saving post data!",
		editor_delete_post: "You really want to delete the current post?",
		editor_delete_post_header: "Delete post",
		editor_delete_success: "Post deleted!",
		editor_delete_error: "Error deleting post!",
		editor_caption_cancel: "Cancel",
		editor_caption_close: "Close"
	};
});


jQuery(document).ready(function() {
	"use strict";
	THEMEREX_GLOBALS['ajax_url'] = 'http://alliance.themerex.net/wp-admin/admin-ajax.php';
	THEMEREX_GLOBALS['ajax_nonce'] = 'e44c6fe2e9';
	THEMEREX_GLOBALS['ajax_nonce_editor'] = '82dffcc9fb';
	THEMEREX_GLOBALS['ajax_login'] = true;
	THEMEREX_GLOBALS['site_url'] = 'http://alliance.themerex.net';
	THEMEREX_GLOBALS['vc_edit_mode'] = false;
	THEMEREX_GLOBALS['theme_font'] = 'Open Sans';
	THEMEREX_GLOBALS['theme_skin'] = 'alliance';
	THEMEREX_GLOBALS['theme_skin_bg'] = '';
	THEMEREX_GLOBALS['slider_height'] = 100;
	THEMEREX_GLOBALS['system_message'] = {
		message: '',
		status: '',
		header: ''
	};
	THEMEREX_GLOBALS['user_logged_in'] = false;
	THEMEREX_GLOBALS['toc_menu'] = 'float';
	THEMEREX_GLOBALS['toc_menu_home'] = false;
	THEMEREX_GLOBALS['toc_menu_top'] = false;
	THEMEREX_GLOBALS['menu_fixed'] = false;
	THEMEREX_GLOBALS['menu_relayout'] = 0;
	THEMEREX_GLOBALS['menu_responsive'] = 0;
	THEMEREX_GLOBALS['menu_slider'] = false;
	THEMEREX_GLOBALS['demo_time'] = 0;
	THEMEREX_GLOBALS['media_elements_enabled'] = true;
	THEMEREX_GLOBALS['ajax_search_enabled'] = true;
	THEMEREX_GLOBALS['ajax_search_min_length'] = 3;
	THEMEREX_GLOBALS['ajax_search_delay'] = 200;
	THEMEREX_GLOBALS['css_animation'] = true;
	THEMEREX_GLOBALS['menu_animation_in'] = '';
	THEMEREX_GLOBALS['menu_animation_out'] = '';
	THEMEREX_GLOBALS['popup_engine'] = 'magnific';
	THEMEREX_GLOBALS['popup_gallery'] = true;
	THEMEREX_GLOBALS['email_mask'] = '^([a-zA-Z0-9_\-]+\.)*[a-zA-Z0-9_\-]+@[a-z0-9_\-]+(\.[a-z0-9_\-]+)*\.[a-z]{2,6}$';
	THEMEREX_GLOBALS['contacts_maxlength'] = 1000;
	THEMEREX_GLOBALS['comments_maxlength'] = 1000;
	THEMEREX_GLOBALS['remember_visitors_settings'] = false;
	THEMEREX_GLOBALS['admin_mode'] = false;
	THEMEREX_GLOBALS['isotope_resize_delta'] = 0.3;
	THEMEREX_GLOBALS['error_message_box'] = null;
	THEMEREX_GLOBALS['viewmore_busy'] = false;
	THEMEREX_GLOBALS['video_resize_inited'] = false;
	THEMEREX_GLOBALS['top_panel_height'] = 0;
});


jQuery(document).ready(function() {
	"use strict";
	if (THEMEREX_GLOBALS['theme_font'] == '') 
		THEMEREX_GLOBALS['theme_font'] = 'Roboto';
		THEMEREX_GLOBALS['link_color'] = '';
		THEMEREX_GLOBALS['menu_color'] = '';
		THEMEREX_GLOBALS['user_color'] = '';
});

jQuery(document).ready(function() {
	"use strict";
	THEMEREX_GLOBALS["reviews_allow_user_marks"] = true;
	THEMEREX_GLOBALS["reviews_max_level"] = 100;
	THEMEREX_GLOBALS["reviews_levels"] = "bad,poor,normal,good,great";
	THEMEREX_GLOBALS["reviews_vote"] = "";
	THEMEREX_GLOBALS["reviews_marks"] = "52,43".split(",");
	THEMEREX_GLOBALS["reviews_users"] = 39;
	THEMEREX_GLOBALS["post_id"] = 543;
});

var WpProQuizGlobal = {
	"ajaxurl": "http:\/\/alliance.themerex.net\/wp-admin\/admin-ajax.php",
	"loadData": "Loading",
	"questionNotSolved": "You must answer this question.",
	"questionsNotSolved": "You must answer all questions before you can completed the quiz.",
	"fieldsNotFilled": "All fields have to be filled."
};
var WpProQuizGlobal = {
	"ajaxurl": "http:\/\/alliance.themerex.net\/wp-admin\/admin-ajax.php",
	"loadData": "Loading",
	"questionNotSolved": "You must answer this question.",
	"questionsNotSolved": "You must answer all questions before you can completed the quiz.",
	"fieldsNotFilled": "All fields have to be filled."
};

var booked_js_vars = {
	"ajax_url": "http:\/\/alliance.themerex.net\/wp-admin\/admin-ajax.php",
	"profilePage": "",
	"publicAppointments": "",
	"i18n_confirm_appt_delete": "Are you sure you want to cancel this appointment?",
	"i18n_please_wait": "Please wait ...",
	"i18n_wrong_username_pass": "Wrong username\/password combination.",
	"i18n_fill_out_required_fields": "Please fill out all required fields.",
	"i18n_guest_appt_required_fields": "Please enter your name to book an appointment.",
	"i18n_appt_required_fields": "Please enter your name, your email address and choose a password to book an appointment.",
	"i18n_appt_required_fields_guest": "Please fill in all \"Information\" fields.",
	"i18n_password_reset": "Please check your email for instructions on resetting your password.",
	"i18n_password_reset_error": "That username or email is not recognized."
};


var php_data = {
	"treeupdate_path_url": "http:\/\/alliance.themerex.net\/wp-content\/plugins\/userpress\/page-tree\/menuSortableSave.php"
};

var subwikisexist = "0";

var ajaxurl = "http:\/\/alliance.themerex.net\/wp-admin\/admin-ajax.php";

var mejsL10n = {
	"language": "en-US",
	"strings": {
		"Close": "Close",
		"Fullscreen": "Fullscreen",
		"Turn off Fullscreen": "Turn off Fullscreen",
		"Go Fullscreen": "Go Fullscreen",
		"Download File": "Download File",
		"Download Video": "Download Video",
		"Play": "Play",
		"Pause": "Pause",
		"Captions\/Subtitles": "Captions\/Subtitles",
		"None": "None",
		"Time Slider": "Time Slider",
		"Skip back %1 seconds": "Skip back %1 seconds",
		"Video Player": "Video Player",
		"Audio Player": "Audio Player",
		"Volume Slider": "Volume Slider",
		"Mute Toggle": "Mute Toggle",
		"Unmute": "Unmute",
		"Mute": "Mute",
		"Use Up\/Down Arrow keys to increase or decrease volume.": "Use Up\/Down Arrow keys to increase or decrease volume.",
		"Use Left\/Right Arrow keys to advance one second, Up\/Down arrows to advance ten seconds.": "Use Left\/Right Arrow keys to advance one second, Up\/Down arrows to advance ten seconds."
	}
};
var _wpmejsSettings = {
	"pluginPath": "\/wp-includes\/js\/mediaelement\/"
};


var EOAjaxFront = {
	"adminajax": "http:\/\/alliance.themerex.net\/wp-admin\/admin-ajax.php",
	"locale": {
		"locale": "en",
		"isrtl": false,
		"monthNames": ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
		"monthAbbrev": ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
		"dayNames": ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
		"dayAbbrev": ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
		"dayInitial": ["S", "M", "T", "W", "T", "F", "S"],
		"ShowMore": "Show More",
		"ShowLess": "Show Less",
		"today": "today",
		"day": "day",
		"week": "week",
		"month": "month",
		"gotodate": "go to date",
		"cat": "View all categories",
		"venue": "View all venues",
		"tag": "View all tags",
		"nextText": ">",
		"prevText": "<"
	}
};
var eventorganiser = {
	"ajaxurl": "http:\/\/alliance.themerex.net\/wp-admin\/admin-ajax.php",
	"calendars": [{
		"headerleft": "title, category, prev, next",
		"headercenter": "",
		"headerright": "",
		"defaultview": "month",
		"aspectratio": false,
		"compact": false,
		"event-category": "",
		"event_category": "",
		"event-venue": "",
		"event_venue": "",
		"event-tag": "",
		"author": false,
		"author_name": false,
		"timeformat": "h:mm a",
		"axisformat": "h:mm a",
		"tooltip": true,
		"weekends": true,
		"mintime": "0:00",
		"maxtime": "24:00",
		"slotduration": "00:30:00",
		"nextdaythreshold": "06:00:00",
		"alldayslot": true,
		"alldaytext": "All day",
		"columnformatmonth": "ddd",
		"columnformatweek": "ddd M\/D",
		"columnformatday": "dddd M\/D",
		"titleformatmonth": "MMMM YYYY",
		"titleformatweek": "MMM D, YYYY",
		"titleformatday": "dddd, MMM D, YYYY",
		"year": false,
		"month": false,
		"date": false,
		"defaultdate": false,
		"users_events": false,
		"event_series": false,
		"event_occurrence__in": [],
		"theme": false,
		"reset": true,
		"isrtl": false,
		"responsive": true,
		"responsivebreakpoint": 514,
		"hiddendays": [],
		"event_tag": "",
		"event_organiser": 0,
		"timeformatphp": "g:i a",
		"axisformatphp": "g:i a",
		"columnformatdayphp": "l n\/j",
		"columnformatweekphp": "D n\/j",
		"columnformatmonthphp": "D",
		"titleformatmonthphp": "F Y",
		"titleformatdayphp": "l, M j, Y",
		"titleformatweekphp": "M j, Y"
	}, {
		"headerleft": "title, category, prev, next",
		"headercenter": "",
		"headerright": "",
		"defaultview": "month",
		"aspectratio": false,
		"compact": false,
		"event-category": "",
		"event_category": "",
		"event-venue": "",
		"event_venue": "",
		"event-tag": "",
		"author": false,
		"author_name": false,
		"timeformat": "h:mm a",
		"axisformat": "h:mm a",
		"tooltip": true,
		"weekends": true,
		"mintime": "0:00",
		"maxtime": "24:00",
		"slotduration": "00:30:00",
		"nextdaythreshold": "06:00:00",
		"alldayslot": true,
		"alldaytext": "All day",
		"columnformatmonth": "ddd",
		"columnformatweek": "ddd M\/D",
		"columnformatday": "dddd M\/D",
		"titleformatmonth": "MMMM YYYY",
		"titleformatweek": "MMM D, YYYY",
		"titleformatday": "dddd, MMM D, YYYY",
		"year": false,
		"month": false,
		"date": false,
		"defaultdate": false,
		"users_events": false,
		"event_series": false,
		"event_occurrence__in": [],
		"theme": false,
		"reset": true,
		"isrtl": false,
		"responsive": true,
		"responsivebreakpoint": 514,
		"hiddendays": [],
		"event_tag": "",
		"event_organiser": 0,
		"timeformatphp": "g:i a",
		"axisformatphp": "g:i a",
		"columnformatdayphp": "l n\/j",
		"columnformatweekphp": "D n\/j",
		"columnformatmonthphp": "D",
		"titleformatmonthphp": "F Y",
		"titleformatdayphp": "l, M j, Y",
		"titleformatweekphp": "M j, Y"
	}],
	"widget_calendars": [],
	"fullcal": {
		"firstDay": 1,
		"venues": [],
		"categories": [],
		"tags": []
	},
	"map": []
};

var bbpTopicJS = {
	"bbp_ajaxurl": "http:\/\/alliance.themerex.net\/forums\/topic\/business-growth-sed-vel-tempus-libero\/?bbp-ajax=true",
	"generic_ajax_error": "Something went wrong. Refresh your browser and try again.",
	"is_user_logged_in": "",
	"fav_nonce": "529d3428cf",
	"subs_nonce": "04b1fb15a1"
};


var BuddyDrive_App = {
	"settings": {
		"buddydrive_scope": "files",
		"privacy_filters": {
			"public": {
				"text": "Public",
				"priority": 5,
				"allow_multiple": false
			},
			"private": {
				"text": "Private",
				"priority": 10,
				"allow_multiple": false
			},
			"password": {
				"text": "Password protected",
				"priority": 15,
				"allow_multiple": false
			},
			"friends": {
				"text": "Restricted to friends",
				"priority": 20,
				"allow_multiple": false
			},
			"groups": {
				"text": "Restricted to a group",
				"priority": 25,
				"allow_multiple": true,
				"autocomplete_placeholder": "Start typing a group name"
			},
			"members": {
				"text": "Restricted to members",
				"priority": 30,
				"allow_multiple": true,
				"autocomplete_placeholder": "Start typing a member name"
			},
			"folder": {
				"text": "Add to an existing folder",
				"priority": 35,
				"allow_multiple": false,
				"autocomplete_placeholder": "Start typing a folder name"
			}
		},
		"loop_filters": {
			"modified": {
				"text": "Last edit",
				"priority": 5
			},
			"title": {
				"text": "Name",
				"priority": 10
			}
		},
		"nonces": {
			"fetch_items": "a9ba055c68",
			"fetch_objects": "9f96898500",
			"update_item": "544b9d31e3",
			"new_folder": "9c30265bdb",
			"bulk_edit": "9df8cbdb3b",
			"user_stats": "e9a1853926"
		}
	},
	"strings": {
		"allCrumb": "All",
		"loadMore": "Load More",
		"privacyFilterLabel": "Select your privacy preferences.",
		"passwordInputLabel": "Define your password.",
		"loopFilterLabel": "Order items by:",
		"editErrors": {
			"title": "Please make sure to provide a name for your file or folder.",
			"groups": "Please make sure to select a group using the autocomplete field.",
			"members": "Please make sure to select a member using the autocomplete field.",
			"folder": "Please make sure to select a folder using the autocomplete field.",
			"password": "Please make sure to provide a password for your file or folder."
		},
		"saveEdits": "Saving your changes, please wait."
	}
};



//////

jQuery(document).ready(function() {
	"use strict";
    if (jQuery("#tl1").length > 0)  
		initTimeline();
    if (jQuery(".comment_text").length > 0)  
		initEmoji();
    if (jQuery(".widget_subcategories").length > 0)  
		initCateg();
    if (jQuery(".isotope_item").length > 0)  
		initFilters();
    if (jQuery(".wpProQuiz_content").length > 0)  
		initQuiz();
    if (jQuery(".esg-grid").length > 0)  
		initEsg();
});

//////


function initEsg() {
	"use strict";
	function eggbfc(winw, resultoption) {
		var lasttop = winw,
			lastbottom = 0,
			smallest = 9999,
			largest = 0,
			samount = 0,
			lamoung = 0,
			lastamount = 0,
			resultid = 0,
			resultidb = 0,
			responsiveEntries = [{
				width: 1400,
				amount: 5,
				mmheight: 0
			}, {
				width: 1170,
				amount: 3,
				mmheight: 0
			}, {
				width: 1024,
				amount: 3,
				mmheight: 0
			}, {
				width: 960,
				amount: 3,
				mmheight: 0
			}, {
				width: 778,
				amount: 3,
				mmheight: 0
			}, {
				width: 640,
				amount: 3,
				mmheight: 0
			}, {
				width: 480,
				amount: 1,
				mmheight: 0
			}];
		if (responsiveEntries != undefined && responsiveEntries.length > 0)
			jQuery.each(responsiveEntries, function(index, obj) {
				var curw = obj.width != undefined ? obj.width : 0,
					cura = obj.amount != undefined ? obj.amount : 0;
				if (smallest > curw) {
					smallest = curw;
					samount = cura;
					resultidb = index;
				}
				if (largest < curw) {
					largest = curw;
					lamount = cura;
				}
				if (curw > lastbottom && curw <= lasttop) {
					lastbottom = curw;
					lastamount = cura;
					resultid = index;
				}
			});
		if (smallest > winw) {
			lastamount = samount;
			resultid = resultidb;
		}
		var obj = new Object;
		obj.index = resultid;
		obj.column = lastamount;
		if (resultoption == "id")
			return obj;
		else
			return lastamount;
	}
	if ("cobbles" == "even") {
		var coh = 0,
			container = jQuery("#esg-grid-1-1");
		var cwidth = container.width(),
			ar = "4:3",
			gbfc = eggbfc(jQuery(window).width(), "id"),
			row = 3;
		ar = ar.split(":");
		aratio = parseInt(ar[0], 0) / parseInt(ar[1], 0);
		coh = cwidth / aratio;
		coh = coh / gbfc.column * row;
		var ul = container.find("ul").first();
		ul.css({
			display: "block",
			height: coh + "px"
		});
	}
	var essapi_1;
	jQuery(document).ready(function() {
		essapi_1 = jQuery("#esg-grid-1-1").tpessential({
			gridID: 1,
			layout: "cobbles",
			forceFullWidth: "off",
			lazyLoad: "off",
			row: 3,
			loadMoreAjaxToken: "52099088e3",
			loadMoreAjaxUrl: "http://alliance.themerex.net/wp-admin/admin-ajax.php",
			loadMoreAjaxAction: "Essential_Grid_Front_request_ajax",
			ajaxContentTarget: "ess-grid-ajax-container-",
			ajaxScrollToOffset: "0",
			ajaxCloseButton: "off",
			ajaxContentSliding: "on",
			ajaxScrollToOnLoad: "on",
			ajaxNavButton: "off",
			ajaxCloseType: "type1",
			ajaxCloseInner: "false",
			ajaxCloseStyle: "light",
			ajaxClosePosition: "tr",
			space: 0,
			pageAnimation: "horizontal-flipbook",
			paginationScrollToTop: "off",
			spinner: "spinner0",
			lightBoxMode: "contentgroup",
			animSpeed: 1000,
			delayBasic: 1,
			mainhoverdelay: 1,
			filterType: "single",
			showDropFilter: "hover",
			filterGroupClass: "esg-fgc-1",
			googleFonts: ['Open+Sans:300,400,600,700,800', 'Raleway:100,200,300,400,500,600,700,800,900', 'Droid+Serif:400,700'],
			aspectratio: "4:3",
			responsiveEntries: [{
				width: 1600,
				amount: 5,
				mmheight: 0
			}, {
				width: 1170,
				amount: 3,
				mmheight: 0
			}, {
				width: 1024,
				amount: 3,
				mmheight: 0
			}, {
				width: 960,
				amount: 3,
				mmheight: 0
			}, {
				width: 778,
				amount: 3,
				mmheight: 0
			}, {
				width: 640,
				amount: 3,
				mmheight: 0
			}, {
				width: 480,
				amount: 1,
				mmheight: 0
			}]
		});

		try {
			jQuery("#esg-grid-1-1 .esgbox").esgbox({
				padding: [0, 0, 0, 0],
				width: 800,
				height: 600,
				minWidth: 100,
				minHeight: 100,
				maxWidth: 9999,
				maxHeight: 9999,
				autoPlay: false,
				playSpeed: 3000,
				preload: 3,
				beforeLoad: function() {},
				afterLoad: function() {
					if (this.element.hasClass("esgboxhtml5")) {
						this.type = "html5";
						var mp = this.element.data("mp4"),
							ogv = this.element.data("ogv"),
							webm = this.element.data("webm");
						ratio = this.element.data("ratio");
						ratio = ratio === "16:9" ? "56.25%" : "75%"
						this.content = '<div class="esg-lb-video-wrapper" style="width:100%"><video autoplay="true" loop=""  poster="" width="100%" height="auto"><source src="' + mp + '" type="video/mp4"><source src="' + webm + '" type="video/webm"><source src="' + ogv + '" type="video/ogg"></video></div>';
					};
				},
				beforeShow: function() {
					this.title = jQuery(this.element).attr('lgtitle');
					if (this.title) {
						this.title = "";
						this.title = '<div style="padding:0px 0px 0px 0px">' + this.title + '</div>';
					}
				},
				afterShow: function() {},
				openEffect: 'fade',
				closeEffect: 'fade',
				nextEffect: 'fade',
				prevEffect: 'fade',
				openSpeed: 'normal',
				closeSpeed: 'normal',
				nextSpeed: 'normal',
				prevSpeed: 'normal',
				helpers: {
					overlay: {
						locked: false
					}
				},
				helpers: {
					media: {},
					overlay: {
						locked: false
					},
					title: {
						type: ""
					}
				}
			});
		} catch (e) {}
	});


	function eggbfc(winw, resultoption) {
		var lasttop = winw,
			lastbottom = 0,
			smallest = 9999,
			largest = 0,
			samount = 0,
			lamoung = 0,
			lastamount = 0,
			resultid = 0,
			resultidb = 0,
			responsiveEntries = [{
				width: 1400,
				amount: 3,
				mmheight: 0
			}, {
				width: 1170,
				amount: 3,
				mmheight: 0
			}, {
				width: 1024,
				amount: 2,
				mmheight: 0
			}, {
				width: 960,
				amount: 3,
				mmheight: 0
			}, {
				width: 778,
				amount: 3,
				mmheight: 0
			}, {
				width: 640,
				amount: 2,
				mmheight: 0
			}, {
				width: 480,
				amount: 1,
				mmheight: 0
			}];
		if (responsiveEntries != undefined && responsiveEntries.length > 0)
			jQuery.each(responsiveEntries, function(index, obj) {
				var curw = obj.width != undefined ? obj.width : 0,
					cura = obj.amount != undefined ? obj.amount : 0;
				if (smallest > curw) {
					smallest = curw;
					samount = cura;
					resultidb = index;
				}
				if (largest < curw) {
					largest = curw;
					lamount = cura;
				}
				if (curw > lastbottom && curw <= lasttop) {
					lastbottom = curw;
					lastamount = cura;
					resultid = index;
				}
			});
		if (smallest > winw) {
			lastamount = samount;
			resultid = resultidb;
		}
		var obj = new Object;
		obj.index = resultid;
		obj.column = lastamount;
		if (resultoption == "id")
			return obj;
		else
			return lastamount;
	}
	
	var aratio;
	if ("even" == "even") {
		var coh = 0,
			container = jQuery("#esg-grid-3-1");
		var cwidth = container.width(),
			ar = "4:3",
			gbfc = eggbfc(jQuery(window).width(), "id"),
			row = 3;
		ar = ar.split(":");
		aratio = parseInt(ar[0], 0) / parseInt(ar[1], 0);
		coh = cwidth / aratio;
		coh = coh / gbfc.column * row;
		var ul = container.find("ul").first();
		ul.css({
			display: "block",
			height: coh + "px"
		});
	}
	var essapi_3;
	jQuery(document).ready(function() {
		essapi_3 = jQuery("#esg-grid-3-1").tpessential({
			gridID: 3,
			layout: "even",
			forceFullWidth: "off",
			lazyLoad: "off",
			row: 3,
			loadMoreAjaxToken: "52099088e3",
			loadMoreAjaxUrl: "http://alliance.themerex.net/wp-admin/admin-ajax.php",
			loadMoreAjaxAction: "Essential_Grid_Front_request_ajax",
			ajaxContentTarget: "ess-grid-ajax-container-",
			ajaxScrollToOffset: "0",
			ajaxCloseButton: "off",
			ajaxContentSliding: "on",
			ajaxScrollToOnLoad: "on",
			ajaxNavButton: "off",
			ajaxCloseType: "type1",
			ajaxCloseInner: "false",
			ajaxCloseStyle: "light",
			ajaxClosePosition: "tr",
			space: 0,
			pageAnimation: "horizontal-flipbook",
			paginationScrollToTop: "off",
			spinner: "spinner0",
			evenGridMasonrySkinPusher: "off",
			lightBoxMode: "contentgroup",
			animSpeed: 1000,
			delayBasic: 1,
			mainhoverdelay: 1,
			filterType: "single",
			showDropFilter: "hover",
			filterGroupClass: "esg-fgc-3",
			googleFonts: ['Open+Sans:300,400,600,700,800', 'Raleway:100,200,300,400,500,600,700,800,900', 'Droid+Serif:400,700'],
			aspectratio: "4:3",
			responsiveEntries: [{
				width: 1400,
				amount: 3,
				mmheight: 0
			}, {
				width: 1170,
				amount: 3,
				mmheight: 0
			}, {
				width: 1024,
				amount: 2,
				mmheight: 0
			}, {
				width: 960,
				amount: 3,
				mmheight: 0
			}, {
				width: 778,
				amount: 3,
				mmheight: 0
			}, {
				width: 640,
				amount: 2,
				mmheight: 0
			}, {
				width: 480,
				amount: 1,
				mmheight: 0
			}]
		});

		try {
			jQuery("#esg-grid-3-1 .esgbox").esgbox({
				padding: [0, 0, 0, 0],
				width: 800,
				height: 600,
				minWidth: 100,
				minHeight: 100,
				maxWidth: 9999,
				maxHeight: 9999,
				autoPlay: false,
				playSpeed: 3000,
				preload: 3,
				beforeLoad: function() {},
				afterLoad: function() {
					if (this.element.hasClass("esgboxhtml5")) {
						this.type = "html5";
						var mp = this.element.data("mp4"),
							ogv = this.element.data("ogv"),
							webm = this.element.data("webm");
						ratio = this.element.data("ratio");
						ratio = ratio === "16:9" ? "56.25%" : "75%"
						this.content = '<div class="esg-lb-video-wrapper" style="width:100%"><video autoplay="true" loop=""  poster="" width="100%" height="auto"><source src="' + mp + '" type="video/mp4"><source src="' + webm + '" type="video/webm"><source src="' + ogv + '" type="video/ogg"></video></div>';
					};
				},
				beforeShow: function() {
					this.title = jQuery(this.element).attr('lgtitle');
					if (this.title) {
						this.title = "";
						this.title = '<div style="padding:0px 0px 0px 0px">' + this.title + '</div>';
					}
				},
				afterShow: function() {},
				openEffect: 'fade',
				closeEffect: 'fade',
				nextEffect: 'fade',
				prevEffect: 'fade',
				openSpeed: 'normal',
				closeSpeed: 'normal',
				nextSpeed: 'normal',
				prevSpeed: 'normal',
				helpers: {
					overlay: {
						locked: false
					}
				},
				helpers: {
					media: {},
					overlay: {
						locked: false
					},
					title: {
						type: ""
					}
				}
			});

		} catch (e) {}

	});
	
	
	
	function eggbfc(winw, resultoption) {
		var lasttop = winw,
			lastbottom = 0,
			smallest = 9999,
			largest = 0,
			samount = 0,
			lamoung = 0,
			lastamount = 0,
			resultid = 0,
			resultidb = 0,
			responsiveEntries = [{
				width: 1400,
				amount: 3,
				mmheight: 0
			}, {
				width: 1170,
				amount: 3,
				mmheight: 0
			}, {
				width: 1024,
				amount: 2,
				mmheight: 0
			}, {
				width: 960,
				amount: 3,
				mmheight: 0
			}, {
				width: 778,
				amount: 3,
				mmheight: 0
			}, {
				width: 640,
				amount: 2,
				mmheight: 0
			}, {
				width: 480,
				amount: 1,
				mmheight: 0
			}];
		if (responsiveEntries != undefined && responsiveEntries.length > 0)
			jQuery.each(responsiveEntries, function(index, obj) {
				var curw = obj.width != undefined ? obj.width : 0,
					cura = obj.amount != undefined ? obj.amount : 0;
				if (smallest > curw) {
					smallest = curw;
					samount = cura;
					resultidb = index;
				}
				if (largest < curw) {
					largest = curw;
					lamount = cura;
				}
				if (curw > lastbottom && curw <= lasttop) {
					lastbottom = curw;
					lastamount = cura;
					resultid = index;
				}
			});
		if (smallest > winw) {
			lastamount = samount;
			resultid = resultidb;
		}
		var obj = new Object;
		obj.index = resultid;
		obj.column = lastamount;
		if (resultoption == "id")
			return obj;
		else
			return lastamount;
	}
	if ("even" == "even") {
		var coh = 0,
			container = jQuery("#esg-grid-4-1");
		var cwidth = container.width(),
			ar = "7:5",
			gbfc = eggbfc(jQuery(window).width(), "id"),
			row = 3;
		ar = ar.split(":");
		aratio = parseInt(ar[0], 0) / parseInt(ar[1], 0);
		coh = cwidth / aratio;
		coh = coh / gbfc.column * row;
		var ul = container.find("ul").first();
		ul.css({
			display: "block",
			height: coh + "px"
		});
	}
	var essapi_4;
	jQuery(document).ready(function() {
		essapi_4 = jQuery("#esg-grid-4-1").tpessential({
			gridID: 4,
			layout: "even",
			forceFullWidth: "off",
			lazyLoad: "off",
			row: 3,
			loadMoreAjaxToken: "52099088e3",
			loadMoreAjaxUrl: "http://alliance.themerex.net/wp-admin/admin-ajax.php",
			loadMoreAjaxAction: "Essential_Grid_Front_request_ajax",
			ajaxContentTarget: "ess-grid-ajax-container-",
			ajaxScrollToOffset: "0",
			ajaxCloseButton: "off",
			ajaxContentSliding: "on",
			ajaxScrollToOnLoad: "on",
			ajaxNavButton: "off",
			ajaxCloseType: "type1",
			ajaxCloseInner: "false",
			ajaxCloseStyle: "light",
			ajaxClosePosition: "tr",
			space: 20,
			pageAnimation: "horizontal-flipbook",
			paginationScrollToTop: "off",
			spinner: "spinner0",
			evenGridMasonrySkinPusher: "off",
			lightBoxMode: "contentgroup",
			animSpeed: 1000,
			delayBasic: 1,
			mainhoverdelay: 1,
			filterType: "single",
			showDropFilter: "hover",
			filterGroupClass: "esg-fgc-4",
			googleFonts: ['Open+Sans:300,400,600,700,800', 'Raleway:100,200,300,400,500,600,700,800,900', 'Droid+Serif:400,700'],
			aspectratio: "7:5",
			responsiveEntries: [{
				width: 1400,
				amount: 3,
				mmheight: 0
			}, {
				width: 1170,
				amount: 3,
				mmheight: 0
			}, {
				width: 1024,
				amount: 2,
				mmheight: 0
			}, {
				width: 960,
				amount: 3,
				mmheight: 0
			}, {
				width: 778,
				amount: 3,
				mmheight: 0
			}, {
				width: 640,
				amount: 2,
				mmheight: 0
			}, {
				width: 480,
				amount: 1,
				mmheight: 0
			}]
		});

		try {
			jQuery("#esg-grid-4-1 .esgbox").esgbox({
				padding: [0, 0, 0, 0],
				width: 800,
				height: 600,
				minWidth: 100,
				minHeight: 100,
				maxWidth: 9999,
				maxHeight: 9999,
				autoPlay: false,
				playSpeed: 3000,
				preload: 3,
				beforeLoad: function() {},
				afterLoad: function() {
					if (this.element.hasClass("esgboxhtml5")) {
						this.type = "html5";
						var mp = this.element.data("mp4"),
							ogv = this.element.data("ogv"),
							webm = this.element.data("webm");
						ratio = this.element.data("ratio");
						ratio = ratio === "16:9" ? "56.25%" : "75%"
						this.content = '<div class="esg-lb-video-wrapper" style="width:100%"><video autoplay="true" loop=""  poster="" width="100%" height="auto"><source src="' + mp + '" type="video/mp4"><source src="' + webm + '" type="video/webm"><source src="' + ogv + '" type="video/ogg"></video></div>';
					};
				},
				beforeShow: function() {
					this.title = jQuery(this.element).attr('lgtitle');
					if (this.title) {
						this.title = "";
						this.title = '<div style="padding:0px 0px 0px 0px">' + this.title + '</div>';
					}
				},
				afterShow: function() {},
				openEffect: 'fade',
				closeEffect: 'fade',
				nextEffect: 'fade',
				prevEffect: 'fade',
				openSpeed: 'normal',
				closeSpeed: 'normal',
				nextSpeed: 'normal',
				prevSpeed: 'normal',
				helpers: {
					overlay: {
						locked: false
					}
				},
				helpers: {
					media: {},
					overlay: {
						locked: false
					},
					title: {
						type: ""
					}
				}
			});

		} catch (e) {}

	});

	
	function eggbfc(winw, resultoption) {
		var lasttop = winw,
			lastbottom = 0,
			smallest = 9999,
			largest = 0,
			samount = 0,
			lamoung = 0,
			lastamount = 0,
			resultid = 0,
			resultidb = 0,
			responsiveEntries = [{
				width: 1400,
				amount: 4,
				mmheight: 0
			}, {
				width: 1170,
				amount: 3,
				mmheight: 0
			}, {
				width: 1024,
				amount: 3,
				mmheight: 0
			}, {
				width: 960,
				amount: 3,
				mmheight: 0
			}, {
				width: 778,
				amount: 3,
				mmheight: 0
			}, {
				width: 640,
				amount: 2,
				mmheight: 0
			}, {
				width: 480,
				amount: 1,
				mmheight: 0
			}];
			var lamount;
		if (responsiveEntries != undefined && responsiveEntries.length > 0)
			jQuery.each(responsiveEntries, function(index, obj) {
				var curw = obj.width != undefined ? obj.width : 0,
					cura = obj.amount != undefined ? obj.amount : 0;
				if (smallest > curw) {
					smallest = curw;
					samount = cura;
					resultidb = index;
				}
				if (largest < curw) {
					largest = curw;
					lamount = cura;
				}
				if (curw > lastbottom && curw <= lasttop) {
					lastbottom = curw;
					lastamount = cura;
					resultid = index;
				}
			});
		if (smallest > winw) {
			lastamount = samount;
			resultid = resultidb;
		}
		var obj = new Object;
		obj.index = resultid;
		obj.column = lastamount;
		if (resultoption == "id")
			return obj;
		else
			return lastamount;
	}
	if ("masonry" == "even") {
		var coh = 0,
			container = jQuery("#esg-grid-5-1");
		var cwidth = container.width(),
			ar = "4:3",
			gbfc = eggbfc(jQuery(window).width(), "id"),
			row = 3;
		ar = ar.split(":");
		aratio = parseInt(ar[0], 0) / parseInt(ar[1], 0);
		coh = cwidth / aratio;
		coh = coh / gbfc.column * row;
		var ul = container.find("ul").first();
		ul.css({
			display: "block",
			height: coh + "px"
		});
	} 
	var essapi_5;
	jQuery(document).ready(function() {
		essapi_5 = jQuery("#esg-grid-5-1").tpessential({
			gridID: 5,
			layout: "masonry",
			forceFullWidth: "off",
			lazyLoad: "off",
			row: 3,
			loadMoreAjaxToken: "52099088e3",
			loadMoreAjaxUrl: "http://alliance.themerex.net/wp-admin/admin-ajax.php",
			loadMoreAjaxAction: "Essential_Grid_Front_request_ajax",
			ajaxContentTarget: "ess-grid-ajax-container-",
			ajaxScrollToOffset: "0",
			ajaxCloseButton: "off",  
			ajaxContentSliding: "on",
			ajaxScrollToOnLoad: "on",
			ajaxNavButton: "off",
			ajaxCloseType: "type1",
			ajaxCloseInner: "false",
			ajaxCloseStyle: "light",
			ajaxClosePosition: "tr",
			space: 0,
			pageAnimation: "horizontal-flipbook",
			paginationScrollToTop: "off",
			spinner: "spinner0",
			lightBoxMode: "contentgroup",
			animSpeed: 1000,
			delayBasic: 1,
			mainhoverdelay: 1,
			filterType: "single",
			showDropFilter: "hover", 
			filterGroupClass: "esg-fgc-5",
			googleFonts: ['Open+Sans:300,400,600,700,800', 'Raleway:100,200,300,400,500,600,700,800,900', 'Droid+Serif:400,700'],
			responsiveEntries: [{
				width: 1600,
				amount: 4,
				mmheight: 0
			}, {
				width: 1170,
				amount: 3,
				mmheight: 0
			}, {
				width: 1024,
				amount: 3,
				mmheight: 0
			}, {
				width: 960,
				amount: 3,
				mmheight: 0
			}, {
				width: 778,
				amount: 3,
				mmheight: 0
			}, {
				width: 640,
				amount: 2,
				mmheight: 0
			}, {
				width: 480,
				amount: 1,
				mmheight: 0
			}]
		});

		try {
			jQuery("#esg-grid-5-1 .esgbox").esgbox({
				padding: [0, 0, 0, 0],
				width: 800,
				height: 600,
				minWidth: 100,
				minHeight: 100,
				maxWidth: 9999,
				maxHeight: 9999,
				autoPlay: false,
				playSpeed: 3000,
				preload: 3,
				beforeLoad: function() {},
				afterLoad: function() {
					if (this.element.hasClass("esgboxhtml5")) {
						this.type = "html5";
						var mp = this.element.data("mp4"),
							ogv = this.element.data("ogv"),
							webm = this.element.data("webm");
						ratio = this.element.data("ratio");
						ratio = ratio === "16:9" ? "56.25%" : "75%"
						this.content = '<div class="esg-lb-video-wrapper" style="width:100%"><video autoplay="true" loop=""  poster="" width="100%" height="auto"><source src="' + mp + '" type="video/mp4"><source src="' + webm + '" type="video/webm"><source src="' + ogv + '" type="video/ogg"></video></div>';
					};
				},
				beforeShow: function() {
					this.title = jQuery(this.element).attr('lgtitle');
					if (this.title) {
						this.title = "";
						this.title = '<div style="padding:0px 0px 0px 0px">' + this.title + '</div>';
					}
				},
				afterShow: function() {},
				openEffect: 'fade',
				closeEffect: 'fade',
				nextEffect: 'fade',
				prevEffect: 'fade',
				openSpeed: 'normal',
				closeSpeed: 'normal',
				nextSpeed: 'normal',
				prevSpeed: 'normal',
				helpers: {
					overlay: {
						locked: false
					}
				},
				helpers: {
					media: {},
					overlay: {
						locked: false
					},
					title: {
						type: ""
					}
				}
			});

		} catch (e) {}

	});

	
};


//

function initQuiz() {
	"use strict";
	window.wpProQuizInitList = window.wpProQuizInitList || [];
	window.wpProQuizInitList.push({
		id: '#wpProQuiz_2',
		init: {
			quizId: 2,
			mode: 3,
			globalPoints: 3,
			timelimit: 0,
			resultsGrade: [0],
			bo: 1024,
			qpp: 0,
			catPoints: [3],
			formPos: 0,
			lbn: "Finish quiz",
			json: {
				"3": {
					"type": "single",
					"id": 3,
					"catId": 0,
					"points": 1,
					"correct": [0, 1, 0]
				},
				"4": {
					"type": "single",
					"id": 4,
					"catId": 0,
					"points": 1,
					"correct": [0, 1, 0, 0]
				},
				"5": {
					"type": "single",
					"id": 5,
					"catId": 0,
					"points": 1,
					"correct": [0, 0, 0, 1]
				}
			}
		}
	});
}


//

function initFilters() {
	"use strict";
	jQuery(document).ready(function() {
		jQuery("#sc_blogger_236 .isotope_filters").append("<a href=\"#\" data-filter=\"*\" class=\"theme_button active\">All</a><a href=\"#\" data-filter=\".flt_44\" class=\"theme_button\">Social</a><a href=\"#\" data-filter=\".flt_45\" class=\"theme_button\">Technology</a><a href=\"#\" data-filter=\".flt_43\" class=\"theme_button\">Company</a><a href=\"#\" data-filter=\".flt_42\" class=\"theme_button\">Finance</a>");
	});
	jQuery(document).ready(function() {
		jQuery("#sc_blogger_676 .isotope_filters").append("<a href=\"#\" data-filter=\"*\" class=\"theme_button active\">All</a><a href=\"#\" data-filter=\".flt_85\" class=\"theme_button\">3d &amp; motion graphics</a><a href=\"#\" data-filter=\".flt_89\" class=\"theme_button\">Web design</a><a href=\"#\" data-filter=\".flt_83\" class=\"theme_button\">Game development</a><a href=\"#\" data-filter=\".flt_84\" class=\"theme_button\">Video</a><a href=\"#\" data-filter=\".flt_87\" class=\"theme_button\">Design &amp; Illustration</a><a href=\"#\" data-filter=\".flt_86\" class=\"theme_button\">Code</a>");
	});
}

//

function initCateg() {
	"use strict";
	jQuery('.widget_subcategories li').each(function() {
		var text = jQuery(this).first().contents().filter(function() {
			return this.nodeType == 3;
		}).text();
		jQuery(this).first().contents().filter(function() {
			return this.nodeType == 3;
		}).replaceWith('');
		text = text.replace(")", "");
		text = text.replace("(", "");
		var count = '<span class="count">' + text + '</span>';
		jQuery(this).append(count);
	});
}


//
	
function initTimeline() {
	"use strict";
	var my_is_mobile_global = 0;
	(function($) {
		var test = false;
		$(window).load(function() {
			if (!test) timeline_init_1($(document));
		});

		function timeline_init_1($this) {
			$this.find(".scrollable-content").mCustomScrollbar();
			$this.find("a[rel^='prettyPhoto']").prettyPhoto();
			$this.find("#tl1").timeline({
				my_show_years: 9,
				my_del: 130,
				my_is_years: 0,
				my_trigger_width: 800,
				my_sizes: {
					"card": {
						"item_width": "255",
						"item_height": "450",
						"margin": "40"
					},
					"active": {
						"item_width": "490",
						"item_height": "450",
						"image_height": "150"
					}
				},
				my_id: 1,
				my_debug: 0,
				is_mobile: 0,
				autoplay: 0,
				autoplay_mob: 0,
				autoplay_step: 10000,
				itemMargin: 40,
				scrollSpeed: 500,
				easing: "easeOutSine",
				openTriggerClass: '.read_more',
				swipeOn: true,
				startItem: "27/05/2015",
				yearsOn: true,
				hideTimeline: false,
				hideControles: false,
				closeText: "Close",
				closeItemOnTransition: false
			});
			$this.find("#tl1").on("ajaxLoaded.timeline", function(e) {
				var scrCnt = e.element.find(".scrollable-content");
				scrCnt.height(scrCnt.parent().height() - scrCnt.parent().children("h2").height() - parseInt(scrCnt.parent().children("h2").css("margin-bottom"),10));
				scrCnt.mCustomScrollbar({
					theme: "light-thin"
				});
				e.element.find("a[rel^='prettyPhoto']").prettyPhoto();
				e.element.find(".timeline_rollover_bottom").timelineRollover("bottom");
			});
		}
	})(jQuery);
};

//

function initEmoji() {
	"use strict";
		window._wpemojiSettings = {
		"baseUrl": "https:\/\/s.w.org\/images\/core\/emoji\/2\/72x72\/",
		"ext": ".png",
		"svgUrl": "https:\/\/s.w.org\/images\/core\/emoji\/2\/svg\/",
		"svgExt": ".svg",
		"source": {
			"concatemoji": "http:\/\/alliance.themerex.net\/wp-includes\/js\/wp-emoji-release.min.js?ver=4.6.6"
		}
	};
	! function(a, b, c) {
		function d(a) {
			var c, d, e, f, g, h = b.createElement("canvas"),
				i = h.getContext && h.getContext("2d"),
				j = String.fromCharCode;
			if (!i || !i.fillText) return !1;
			switch (i.textBaseline = "top", i.font = "600 32px Arial", a) {
				case "flag":
					return i.fillText(j(55356, 56806, 55356, 56826), 0, 0), !(h.toDataURL().length < 3e3) && (i.clearRect(0, 0, h.width, h.height), i.fillText(j(55356, 57331, 65039, 8205, 55356, 57096), 0, 0), c = h.toDataURL(), i.clearRect(0, 0, h.width, h.height), i.fillText(j(55356, 57331, 55356, 57096), 0, 0), d = h.toDataURL(), c !== d);
				case "diversity":
					return i.fillText(j(55356, 57221), 0, 0), e = i.getImageData(16, 16, 1, 1).data, f = e[0] + "," + e[1] + "," + e[2] + "," + e[3], i.fillText(j(55356, 57221, 55356, 57343), 0, 0), e = i.getImageData(16, 16, 1, 1).data, g = e[0] + "," + e[1] + "," + e[2] + "," + e[3], f !== g;
				case "simple":
					return i.fillText(j(55357, 56835), 0, 0), 0 !== i.getImageData(16, 16, 1, 1).data[0];
				case "unicode8":
					return i.fillText(j(55356, 57135), 0, 0), 0 !== i.getImageData(16, 16, 1, 1).data[0];
				case "unicode9":
					return i.fillText(j(55358, 56631), 0, 0), 0 !== i.getImageData(16, 16, 1, 1).data[0]
			}
			return !1
		}

		function e(a) {
			var c = b.createElement("script");
			c.src = a, c.type = "text/javascript", b.getElementsByTagName("head")[0].appendChild(c)
		}
		var f, g, h, i;
		for (i = Array("simple", "flag", "unicode8", "diversity", "unicode9"), c.supports = {
				everything: !0,
				everythingExceptFlag: !0
			}, h = 0; h < i.length; h++) c.supports[i[h]] = d(i[h]), c.supports.everything = c.supports.everything && c.supports[i[h]], "flag" !== i[h] && (c.supports.everythingExceptFlag = c.supports.everythingExceptFlag && c.supports[i[h]]);
		c.supports.everythingExceptFlag = c.supports.everythingExceptFlag && !c.supports.flag, c.DOMReady = !1, c.readyCallback = function() {
			c.DOMReady = !0
		}, c.supports.everything || (g = function() {
			c.readyCallback()
		}, b.addEventListener ? (b.addEventListener("DOMContentLoaded", g, !1), a.addEventListener("load", g, !1)) : (a.attachEvent("onload", g), b.attachEvent("onreadystatechange", function() {
			"complete" === b.readyState && c.readyCallback()
		})), f = c.source || {}, f.concatemoji ? e(f.concatemoji) : f.wpemoji && f.twemoji && (e(f.twemoji), e(f.wpemoji)))
	}(window, document, window._wpemojiSettings);
};