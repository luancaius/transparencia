require_relative "boot"

require "rails"
require "active_model/railtie"
# require "active_record/railtie"  # Comment or remove this line to skip Active Record
require "active_job/railtie"
require "action_controller/railtie"
require "action_mailer/railtie"
require "action_view/railtie"
require "action_cable/engine"
require "rails/test_unit/railtie"  # Include if you’re using Rails’ test framework

Bundler.require(*Rails.groups)

module Crawler
  class Application < Rails::Application
    # Initialize configuration defaults for originally generated Rails version.
    config.load_defaults 8.0

    # Specify any directories to ignore for autoloading if needed.
    config.autoload_lib(ignore: %w[assets tasks])

    # Other configuration settings…
  end
end
